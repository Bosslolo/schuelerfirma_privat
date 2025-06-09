In order to send an invoice to each customer at the end of the month, we are going to implement the itslearning RESTful API to automate this process. Take a look at the [itslearning RestApi Help Page.](https://www.itslearning.com/restapi/help)

## Postman Workspace

![[Pasted image 20250216162437.png]]

For easier collaboration, I created a postman workspace. Here is the invite link: [Postman invite](https://app.getpostman.com/join-team?invite_code=b7b820fba5b804838d3010cc814dbb76c7982e08dd8b1eb2fff41a897d43aadc&target_code=92fbe65d6bdddef0a5166de7a4703b78) 
It includes almost all requests you need to check permission, create an access token, send instant messages, upload files, etc.


## Requirements

Requirements for the invoice: 
- Format: PDF 
- Overview about consumption:
	- Each drink separately reported. 
	- Global consumption rank.
- Accepted payment options 
- Sent to each customer individually

Additional requirements: 
- An individual itslearning course for all customers. 

Monthly sequence: 
1. First we create the invoices for each customer.

2. Then we create an access token using the conventional technique. 
	`POST restapi/oauth2/token`

3. Now we check if the user account has access to the itslearning instant message system. 
	 `GET restapi/personal/instantmessages/permissions/v1`

4. If the user account has access, we get the user id of the respective recipient from the database.. 
	`GET restapi/personal/courses/addparticipants/search/v1?searchText={searchText}`

5. Now, we check if we are allowed to communicate with that person. 
	`GET restapi/personal/instantmessages/privacy/:personId/v1`

6. At that point we can upload the invoice pdf file to itslearning's temporary storage. 
	`POST restapi/personal/instantmessages/attachment/v1`

7. Finally we send the invoice by calling this API endpoint for each user. An individual message gets sent to each customer. 
	`POST restapi/personal/instantmessages/v2`

8. Access token gets revoked. 
	`DELETE restapi/oauth2/token/v1`

############