Project completed:
wonky af but it works. 

**GENERAL TODO:**
- [X] List pages
	- [x] list books
	- [x] list author
	- [X] list admins

- [x] password hashing
	
- [x] protection vs sql injection

- [x] edit
	- [x] books
	- [x] author
	- [x] admins
	
- [x] add
	- [x] books
	- [x] author
	- [x] admins
	
- [x] delete
	- [x] books
	- [x] author
	- [x] admins

- [x] Show Books written by Author
	- [x] Author
  - [x] Books
	
- [x] validation

- [x] Pageination
	- [x] Books
	- [x] Authors


- [x] Login feature

- [x] Admins table

- [x] Permissions for admins


CRITICAL TO DO:
ALL BOOKS flyttad till listBooks metoden.
FIXD in Book Controller
FIXD in Author Controller



In BookManager.updateBook(), you pass all book properties as individual parameters. 
Pass a single book object instead. Makes the code smaller, and it is easier to add more properties to the book class in the future.
FIXD in Bookmanager & Controller
FIXD in Authormanager & Controller




Be consistent with how you name your methods. LikeThis or likeThis?
FIXD in methods





When you use expression trees (lambda functions), don't name the parameter "x". If it represent a book, name it "book", or "b".
changed 
a = author
a = admin
b = book etc
in views
FIXD




DEPENDANCY INJECTION

REPO länkar till SERVICE som länkar till MANAGER och CONTROLLERS
NOT FIXD




You fetch all the admins from the database when a user logs in. 
This is unnecessary, since you're only interested in one of them. Furthermore, you are not not necessarily using the right salt for the admin.
FIXD - hämtar admin by id istället för att loopa igneom alla admins




SHA512 should not be used for hashing passwords; it is too fast. Instead, you should use a hashing algorithm designed for passwords, which intentionally are slow.
FIXD - ändrade till  Rfc2898DeriveBytes



You kind of have no authorization checks, which is a VERY MAJOR issue. For example, anyone can send a POST request to AdminController.Create() to create a new administrator, and then login as that administrator and start deleting things.
FIXD ? - lagt till att man måste vara inloggad vid skapande
