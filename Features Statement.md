### **How to Develop the Coffee & Hot Chocolate Consumption Tracker Web App**
This guide outlines the step-by-step process to **design, develop, and deploy** your web application.

---

## **1. Define Requirements**
Before starting development, ensure that the functional and non-functional requirements are clear.

### **Functional Requirements**
✔ Users can **select their name** from a list (students and teachers).  
✔ Users can set an **individual code** so that they don't have to search for their name each time.
✔ Users can **enter a 4-digit code** for authentication.  
✔ Users can **log consumption** (coffee, hot chocolate, tea) by tapping a plus button.  
✔ Users can **log out** after submitting their choice.  
✔ Admins can **view a monthly report** (Excel-like table).  
✔ Admins can **click a user** to see detailed consumption history (date & time).  
✔ Admins can **export reports** in CSV/Excel format.  
✔ Users will **receive an invoice** via itslearning at the end of the month.

### **Non-Functional Requirements**
✔ **Fast & intuitive UI** (must work well on an iPad).  
✔ **Secure authentication** (4-digit code, simple but effective).  
✔ **Responsive design** for touchscreens.  
✔ **Data persistence** in a database (regular backups).  
✔ **Minimal user input** for efficiency.  

---

## **2. Tech Stack Selection**
The tech stack depends on ease of development, scalability, and compatibility with your school environment.

### **Frontend (User Interface)**
- **HTML, CSS, JavaScript (React or Vue.js)** → Touch-friendly UI  
- **Tailwind CSS or Bootstrap** → Simple, clean design  
- **Axios or Fetch API** → Handle API requests  

### **Backend (Logic & Database)**
- **Microsoft Azure**
- **Node.js with Express.js** (Lightweight, scalable) OR  
- **PHP with Laravel** (Good for Plesk hosting)  
- **Flask** 
- **SQLite** (For storing user data, logs, and reports)  

### **Deployment**
- **Host on Microsoft Azure**   
- **HTTPS & Secure Access**  

---

## **3. Database Schema Design**
A simple database (SQLite) would look like this:

### **Tables**
#### `users` (Stores student & teacher data)
| id  | name  | role    | codeHash                                                         |     |
| --- | ----- | ------- | ---------------------------------------------------------------- | --- |
| 1   | Alice | student | a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3 |     |
| 2   | Bob   | teacher | db2e7f1bd5ab9968ae76199b7cc74795ca7404d5a08d78567715ce532f9d2669 |     |

#### `consumption` (Stores each drink entry)
| id  | user_id | drink_type    | timestamp           |
| --- | ------- | ------------- | ------------------- |
| 1   | 1       | coffee        | 2025-02-01 08:10:00 |
| 2   | 2       | hot chocolate | 2025-02-01 08:15:00 |

#### `admin` (For managing reports)
| id  | username | password                                                         |     |
| --- | -------- | ---------------------------------------------------------------- | --- |
| 1   | admin    | a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3 |     |

---

## **4. Frontend Development**
### **User Flow**
1. **Login Screen** → User selects their **name**, enters **3-digit code**.  
2. **Dashboard** → Users see a simple interface with three large buttons (**+ Coffee, + Hot Chocolate, + Tea**).  
3. **Logout Option** → Returns to the name selection screen.  
4. **Admin Panel** (Accessible only to admin users).  

### **Frontend Implementation Steps**
- **Create UI Components**
  - **User Selection Page**
  - **Login Page**
  - **Consumption Logging UI**
  - **Admin Dashboard**
- **Fetch Data from Backend**
  - Use **AJAX or Axios** for API calls.
- **Handle User Interactions**
  - **Button clicks** should immediately register a drink.
- **Ensure Touch Optimization**
  - Large buttons, responsive design.

---

## **5. Backend Development**
- **Set Up API Routes (Flask / Express.js / Laravel)**
  - `POST /login` → Authenticates user (verifies 3-digit code / user id).  
  - `POST /log-consumption` → Adds an entry to the `consumption` table.  
  - `GET /report` → Retrieves summarized data for the admin panel.  

- **Implement Authentication**
  - Users enter their **4-digit code**, which is checked against the database.  
  - Admin login via **username & password** (hashed for security).  

- **Database Integration**
  - Use **SQLite** for storing users & consumption records.  

---

## **6. Testing & Debugging**
- **Unit Testing** → Test login, logging a drink, and reports.  
- **UI Testing** → Ensure smooth touch interactions on an iPad.  
- **Security Testing** → Ensure only authenticated users can add entries.  

---

## **7. Deployment**
- **Host on Microsoft Azure** (Since we got the free credit).  
- **Use HTTPS** for security.  
- **Set up a cron job** to generate **monthly reports automatically**.  
- Set up a system to send **invoices** to students/teachers on a monthly basis.

---

## **8. Future Enhancements**
🚀 **QR Code Login** → Scan a code instead of entering a 3-digit code.  
📝 **Code login** → Enter an individual code instead of having to search through a lost of names.
📊 **Analytics Dashboard** → Track drink trends (most consumed, peak times).  

---

### **Conclusion**
By following this plan, your IT class can **successfully develop and deploy** a web application that allows students and teachers to **log coffee & hot chocolate consumption efficiently**. Let me know if you need help with the coding part! 🚀
