using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schülerfirma_Admin_Dashboard;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;


// Port of the itslearning.py file with some improvements

namespace itslearningTest
{
    internal class Itslearning
    {
        HttpClient client = new HttpClient();

        public Itslearning()
        {
            client.BaseAddress = new Uri("https://csh.itslearning.com/restapi/");
            client.Timeout = TimeSpan.FromSeconds(1000);
        }

        // If a method returns "EA" in index 0 it means that the access token is invalid and needs to be refreshed

        /* 
         ACCESS TOKEN
         */

        /// <summary>
        ///  Creates the Access Token; Returns a string array with length 4
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="client_id"></param>
        /// <param name="grant_type"></param>
        /// <returns> A string array { "S", access_token, refresh_token, expires_in } </returns>
        public async Task<Dictionary<string, object>> DSOAuth2Token(string username, string password, string client_id = "10ae9d30-1853-48ff-81cb-47b58a325685",
            string grant_type = "password")
        {
            // Validate Input Data 
            // if(username == null) throw new ArgumentNullException("Username is null");   // The excpetion messages are never actually used
            // else if (password== null) throw new ArgumentNullException("Password is null");

            FormUrlEncodedContent content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "client_id" , client_id },
                    { "grant_type" , grant_type },
                    { "username" , username },
                    { "password" , password }
                });

            // HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://csh.itslearning.com/restapi/oauth2/token?client_id=" + client_id + "&grant_type=" + grant_type + "&username=" + username + "&password=" + password);

            try
            {
                var response = await client.PostAsync("oauth2/token", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException();
                response.EnsureSuccessStatusCode();

                Dictionary<string, object> tokens = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                return tokens;
            }
            catch (HttpRequestException) { throw new HttpRequestException(); }
        }


        /// <summary>
        ///  Refreshes the Access Token; Returns a string array with length 4
        /// </summary>
        /// <param name="oauth2token"></param>
        /// <param name="client_id"></param>
        /// <param name="grant_type"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> DSORefreshToken(string oauth2token, string client_id = "10ae9d30-1853-48ff-81cb-47b58a325685", string grant_type = "refresh_token")
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id" , client_id },
                { "grant_type" , grant_type },
                { "refresh_token" , oauth2token }
            });

            try
            {
                var response = await client.PostAsync("oauth2/token", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();

                Dictionary<string, object> refresh_tokens = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                return refresh_tokens;
            }
            catch (HttpRequestException) { 
                MessageBox.Show("Ungültige Anmeldung. Sie werden abgemeldet.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                AccountSelecterView accountSelecterView = new AccountSelecterView();
                accountSelecterView.ShowDialog();
                return new Dictionary<string, object> { };
            }
        }

        /// <summary>
        /// Bietet einen einfachen Weg, immer den aktuellen Zugriffsschlüssel zu erhalten.
        /// </summary>
        /// <returns>string</returns>
        public async Task<string> SEasyAccessToken()
        {
            try
            {
                string at = Schülerfirma_Admin_Dashboard.Properties.Settings.Default.access_token;
                if (at == String.Empty)
                {
                    MessageBox.Show("Sie wurden abgemeldet, weil der Zugriffsschlüssel nicht gültig, abgelaufen oder leer ist. Melden Sie sich erneut an, indem Sie auf „OK“ klicken.", "Fehler beim Anmelden", MessageBoxButton.OK, MessageBoxImage.Warning);
                    AccountSelecterView accountSelecterView = new AccountSelecterView();
                    accountSelecterView.ShowDialog();
                    return "User prompted to authenticate again.";
                }
                bool valid = await ValidateCredsAsync("personal/instantmessages/permissions/v1", at);

                if (valid)
                    return at;
                else
                {
                    at = String.Empty;
                    string rt = Schülerfirma_Admin_Dashboard.Properties.Settings.Default.refresh_token;
                    if (rt == String.Empty)
                    {
                        MessageBox.Show("Sie wurden abgemeldet, weil der Zugriffsschlüssel nicht gültig, abgelaufen oder leer ist. Melden Sie sich erneut an, indem Sie auf „OK“ klicken.", "Fehler beim Anmelden", MessageBoxButton.OK, MessageBoxImage.Warning);
                        throw new UnauthorizedAccessException();
                    }
                    Dictionary<string, object> new_tokens = await DSORefreshToken(rt);
                    Schülerfirma_Admin_Dashboard.Properties.Settings.Default.access_token = (string)new_tokens["access_token"]; // Store the access token in settings for later use. 
                    Schülerfirma_Admin_Dashboard.Properties.Settings.Default.Save();
                    Schülerfirma_Admin_Dashboard.Properties.Settings.Default.refresh_token = (string)new_tokens["refresh_token"]; // Store the refresh token in settings for later use.
                    Schülerfirma_Admin_Dashboard.Properties.Settings.Default.Save();

                    return (string)new_tokens["access_token"];
                }
            }
            catch (UnauthorizedAccessException)
            {
                AccountSelecterView accountSelecterView = new AccountSelecterView();
                accountSelecterView.ShowDialog();
                return "User prompted to authenticate again.";
            }
        }

        public async Task<Dictionary<string, object>> DSORevokeAccessToken()
        {
            try
            {
                var response = await client.DeleteAsync("oauth2/token/v1");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();

                Dictionary<string, object> deleted = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                return deleted;
            }
            catch (HttpRequestException) { throw new HttpRequestException(); }
        }


        /*  
         SSO Url
         */

        /// <summary>
        ///  Creates a Single-Sign-On-Url; Returns a string array with length 3
        /// </summary>
        /// <param name="url"></param>
        /// <param name="oauth2"></param>
        /// <returns></returns>
        public async Task<string> SASSOUrl(string url, string oauth2)
        {
            try
            {
                var response = await client.GetAsync("personal/sso/url/v1?url=" + url + "&access_token=" + oauth2);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();

                JsonDocument sso = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                string sso_url = sso.RootElement.GetProperty("Url").ToString();
                return sso_url;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
            /*
            catch (InvalidOperationException ex) { return new string[3] { "E", ExceptionRes.InvalidOperationExceptionMsg, ex.Message.ToString() }; }
            catch (HttpRequestException ex) { return new string[3] { "E", ExceptionRes.DefaultExceptionMsg, ex.Message.ToString() }; }
            catch (TaskCanceledException ex) { return new string[3] { "E", ExceptionRes.InvalidOperationExceptionMsg, ex.Message.ToString() }; }
            catch (System.Text.Json.JsonException ex) { return new string[3] { "E", ExceptionRes.JsonExceptionMsg, ex.Message.ToString() }; }
            catch (ArgumentNullException ex) { return new string[3] { "E", ExceptionRes.DefaultExceptionMsg, ex.Message.ToString() }; }
            catch (ArgumentException ex) { return new string[3] { "E", ExceptionRes.JsonExceptionMsg, ex.Message.ToString() }; } 
            */
        }

        public async Task<bool> ValidateCredsAsync(string test_url, string oAuth2)
        {
            var request = await client.GetAsync(test_url + "?access_token=" + oAuth2);
            if (request.StatusCode == System.Net.HttpStatusCode.OK) { return true; }
            return false;
        }


        /*  
         PERSON INFORMATION
         */

        public async Task<Dictionary<string, object>> DSOPersonFeatures(string oauth2)
        {
            // https://www.itslearning.com/restapi/help/Api/GET-restapi-personal-person-features-v1

            try
            {
                var response = await client.GetAsync($"personal/person/features/v1?access_token={oauth2}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();

                Dictionary<string, object> course_card_settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                return course_card_settings;
            }
            catch (InvalidOperationException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
            catch (HttpRequestException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.DefaultExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
            catch (TaskCanceledException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
        }

        public async Task<Dictionary<string, object>> DSOProfileImageDimension(string oauth2)
        {
            // https://www.itslearning.com/restapi/help/Api/GET-restapi-personal-person-image-dimensions-v1

            try
            {
                var response = await client.GetAsync($"personal/person/image/dimensions/v1?access_token={oauth2}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();

                Dictionary<string, object> course_card_settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                return course_card_settings;
            }
            catch (InvalidOperationException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
            catch (HttpRequestException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.DefaultExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
            catch (TaskCanceledException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
        }

        public async Task<Dictionary<string, object>> DSOUpdateOnlineStatus(string oauth2)
        {
            // https://www.itslearning.com/restapi/help/Api/GET-restapi-personal-person-image-dimensions-v1

            try
            {
                var response = await client.GetAsync($"personal/person/online/v1?access_token={oauth2}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();

                Dictionary<string, object> course_card_settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                return course_card_settings;
            }
            catch (InvalidOperationException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
            catch (HttpRequestException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.DefaultExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
            catch (TaskCanceledException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
        }

        /* 
         * PASSWORD RESET EMAIL
         */

        public async Task<string[]> SAPasswordResetEmail(string email_address)
        {
            FormUrlEncodedContent content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "email", email_address }
                });
            try
            {
                var response = await client.PostAsync("forgottenpassword/api/email", content);
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return new string[3] { "S", ExceptionRes.PasswordResetEmailSuccessMsg, null };
                else
                    return new string[3] { "E", ExceptionRes.PasswordResetEmailErrorMsg, null };
            }
            catch (InvalidOperationException ex) { return new string[3] { "E", ExceptionRes.InvalidOperationExceptionMsg, ex.Message.ToString() }; }
            catch (HttpRequestException ex) { return new string[3] { "E", ExceptionRes.DefaultExceptionMsg, ex.Message.ToString() }; }
            catch (TaskCanceledException ex) { return new string[4] { "E", ExceptionRes.InvalidOperationExceptionMsg, ex.Message.ToString(), null }; }
        }

        /* 
         * COURSES
         */

        public async Task<Dictionary<string, object>> DSOCourseFeatures(string courseId, string oauth2)
        {
            // https://www.itslearning.com/restapi/help/Api/GET-restapi-personal-courses-courseId-features-v2

            try
            {
                var response = await client.GetAsync($"personal/courses/{courseId}/features/v2?access_token={oauth2}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();

                Dictionary<string, object> course_card_settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                return course_card_settings;
            }
            catch (InvalidOperationException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
            catch (HttpRequestException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.DefaultExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
            catch (TaskCanceledException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
        }

        public async Task<Dictionary<string, object>> DSOCourses(string oauth2, int PageIndex = 500, int PageSize = 500)
        {
            try
            {
                var response = await client.GetAsync($"personal/courses/v2?PageIndex={PageIndex}&PageSize={PageSize}&access_token={oauth2}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();

                Dictionary<string, object> courses = JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                return courses;
            }
            catch (InvalidOperationException ex) { return new Dictionary<string, object> { { "E", ExceptionRes.InvalidOperationExceptionMsg }, { "Msg", ex.Message.ToString() } }; }
        }


        public async Task<JObject> JOGetCustomObject(string oauth2, string url)
        {
            try
            {
                if (url.Contains("?"))
                    url += $"&access_token={oauth2}";
                else
                    url += $"?access_token={oauth2}";
                var response = await client.GetAsync($"{url}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Zugriffsschlüssel ist nicht gültig, abgelaufen oder leer.");
                response.EnsureSuccessStatusCode();
                string resp = await response.Content.ReadAsStringAsync();
                JObject custom = JObject.Parse(await response.Content.ReadAsStringAsync());
                return custom;
            }
            catch (JsonReaderException ex) { throw new JsonReaderException("Die Antwort konnte nicht gelesen werden. Bitte versuchen Sie es später erneut. Weitere Informationen:", ex); }
            catch (InvalidOperationException ex) { throw new InvalidOperationException("Es ist ein Fehler aufgetreten. Bitte versuchen Sie es später erneut. Weitere Informationen:", ex); }
            catch (HttpRequestException ex) { throw new HttpRequestException("Ihre Anfrage konnte nicht bearbeitet werden, wegen: ", ex); }
        }

        public async Task<JArray> JAGetCustomArray(string oauth2, string url)
        {
            try
            {
                if (url.Contains("?"))
                    url += $"&access_token={oauth2}";
                else
                    url += $"?access_token={oauth2}";
                var response = await client.GetAsync($"{url}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Zugriffsschlüssel ist nicht gültig, abgelaufen oder leer.");
                response.EnsureSuccessStatusCode();
                JArray custom = JArray.Parse(await response.Content.ReadAsStringAsync());
                return custom;
            }
            catch (JsonReaderException ex) { throw new JsonReaderException("Die Antwort konnte nicht gelesen werden. Bitte versuchen Sie es später erneut. Weitere Informationen:", ex); }
            catch (InvalidOperationException ex) { throw new InvalidOperationException("Es ist ein Fehler aufgetreten. Bitte versuchen Sie es später erneut. Weitere Informationen:", ex); }
            catch (HttpRequestException ex) { throw new HttpRequestException("Ihre Anfrage konnte nicht bearbeitet werden, wegen: ", ex); }
        }

        public async Task<JObject> JOPostCustom(string oauth2, string url, StringContent content)
        {
            try
            {
                if (url.Contains("?"))
                    url += $"&access_token={oauth2}";
                else
                    url += $"?access_token={oauth2}";
                var response = await client.PostAsync($"{url}", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                response.EnsureSuccessStatusCode();
                JObject custom = JObject.Parse(await response.Content.ReadAsStringAsync());
                return custom;
            }
            catch (InvalidOperationException ex) { throw new InvalidOperationException("Es ist ein Fehler aufgetreten. Bitte versuchen Sie es später erneut. Weitere Informationen:", ex); }
            catch (HttpRequestException ex) { throw new HttpRequestException("Ihre Anfrage konnte nicht bearbeitet werden, wegen: ", ex); }
        }

        public async Task<JArray> JAUploadFileAttachments(string oauth2, List<string> paths)
        {
            // Not sure if this actually works.
            try
            {
                JArray attachments = new JArray();
                foreach (string path in paths)
                {
                    using (var fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        var content = new MultipartFormDataContent();
                        content.Add(new StreamContent(fileStream), "file", System.IO.Path.GetFileName(path));
                        var response = await client.PostAsync($"personal/instantmessages/attachment/v1?access_token={oauth2}", content);
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            throw new UnauthorizedAccessException(ExceptionRes.UnauthorizedAccessExceptionMsg);
                        response.EnsureSuccessStatusCode();
                        JObject attachment = JObject.Parse(await response.Content.ReadAsStringAsync());
                        attachments.Add(attachment);
                    }
                }
                return attachments;
            }
            catch (InvalidOperationException ex) { throw new InvalidOperationException("Es ist ein Fehler aufgetreten. Bitte versuchen Sie es später erneut. Weitere Informationen:", ex); }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Ihre Anfrage konnte nicht bearbeitet werden, wegen: ", ex);
            }
        }
    }
}
