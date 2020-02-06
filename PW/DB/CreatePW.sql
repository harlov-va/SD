USE PW;
GO


--IF OBJECT_ID('dbo.pw_senderRecipient','U') IS NOT NULL
--	DROP TABLE dbo.pw_senderRecipient;
--GO

--IF OBJECT_ID('dbo.pw_recipientTransactions','U') IS NOT NULL
--	DROP TABLE dbo.pw_recipientTransactions;
--GO

--IF OBJECT_ID('dbo.pw_senderTransactions','U') IS NOT NULL
--	DROP TABLE dbo.pw_senderTransactions;
--GO

IF OBJECT_ID('dbo.pw_transactions','U') IS NOT NULL
	DROP TABLE dbo.pw_transactions;
GO

IF OBJECT_ID('dbo.pw_tokens','U') IS NOT NULL
	DROP TABLE dbo.pw_tokens;
GO

IF OBJECT_ID('dbo.pw_users','U') IS NOT NULL
	DROP TABLE dbo.pw_users;
GO




CREATE TABLE pw_users(
id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
userName	NVARCHAR(100) NOT NULL,
[password]	NVARCHAR(100) NOT NULL,
email	NVARCHAR(50) NOT NULL,
balance	MONEY NOT NULL,
dateCreate	DATETIME NOT NULL
)
GO

INSERT INTO pw_users(userName ,[password] ,email,balance,dateCreate) VALUES
('Nikki Alvarez','23','23',500,GETDATE()),
('Quinn Armitage','pasw','QuinnArmitage@gmail.com',600,GETDATE()),
('Robert Barr','pasw','RobertBarr@gmail.com',510,GETDATE()),
('Laura Simmons Asher','pasw','LauraAsher@gmail.com',500,GETDATE()),
('Flame Beaufort','pasw','FlameBeaufort@gmail.com',390,GETDATE()),
('Eden Capwell','pasw','EdenCapwell@gmail.com',500,GETDATE()),
('Julia Wainwright Capwell','pasw','JuliaCapwell@gmail.com',500,GETDATE()),
('Lily Blake Capwell','pasw','LilyCapwell@gmail.com',500,GETDATE()),
('Kelly Capwell','pasw','KellyCapwell@gmail.com',500,GETDATE()),
('Ted Capwell','pasw','Ted2Capwell@gmail.com',500,GETDATE())
;
GO

CREATE TABLE pw_tokens(
id	INT PRIMARY KEY  IDENTITY(1,1) NOT NULL,
[id_token]	NVARCHAR(MAX) NOT NULL,
usersID INT NOT NULL REFERENCES pw_users(id) ON DELETE CASCADE
);
GO

INSERT INTO pw_tokens([id_token], usersID) VALUES
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',1),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',2),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',3),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',4),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',5),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',6),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',7),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',8),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',9),
('eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VybmFtZSI6IkFuaW1hbCBQbGFuZXQgVXNlciIsImVtYWlsIjoiMUAxLjEiLCJpZCI6MywiYmFsYW5jZSI6NTAwLCJpYXQiOjE0ODE1ODQ4ODksImV4cCI6MTQ4MTYwMjg4OX0.h4CzCxTOMRk6S8juxM0tRc5pql99XkXlR09pUzVMH9I',10)
;
GO


CREATE TABLE pw_transactions(		
id	INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[date]	DATETIME NOT NULL,
amount	MONEY NOT NULL,
balance	MONEY NOT NULL,
usersID INT NOT NULL REFERENCES pw_users(id) ON DELETE CASCADE,
userName	NVARCHAR(100) NOT NULL
);
GO

INSERT INTO pw_transactions([date] ,amount,balance ,usersID, userName) VALUES
(GETDATE(),-250,250,1,'Quinn Armitage'),
(GETDATE(),250,750,2,'23'),
(GETDATE(),-10,240,1,'Robert Bar'),
(GETDATE(),10,510,3,'23'),
(GETDATE(),-150,600,2,'23'),
(GETDATE(),150,390,1,'Quinn Armitage'),
(GETDATE(),-110,390,5,'23'),
(GETDATE(),110,500,1,'Flame Beaufort')
;
GO

--CREATE TABLE pw_senderTransactions(		
--id	INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
--[date]	DATETIME NOT NULL,
--amount	MONEY NOT NULL,
--balance	MONEY NOT NULL,
--usersID INT NOT NULL REFERENCES pw_users(id) ON DELETE CASCADE,
--);
--GO

--INSERT INTO pw_senderTransactions([date] ,amount,balance ,usersID) VALUES
--(GETDATE(),250,250,1),
--(GETDATE(),50,450,5)
--;
--GO

--CREATE TABLE pw_recipientTransactions(
--id	INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
--[date]	DATETIME NOT NULL,
--amount	MONEY NOT NULL,
--balance	MONEY NOT NULL,
--usersID INT NOT NULL REFERENCES pw_users(id) ON DELETE CASCADE
--);
--GO

--INSERT INTO pw_recipientTransactions([date] ,amount,balance ,usersID) VALUES
--(GETDATE(),250,750,2),
--(GETDATE(),50,550,6)
--;
--GO

--CREATE TABLE pw_senderRecipient(
--id	INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
--senderTransactionsID INT NOT NULL REFERENCES pw_senderTransactions(id) ON DELETE NO ACTION,
--recipientTransactionsID INT NOT NULL REFERENCES pw_recipientTransactions(id) ON DELETE NO ACTION
--);
--GO

--INSERT INTO pw_senderRecipient(senderTransactionsID ,recipientTransactionsID) VALUES
--(1,1),
--(2,2)
--;
--GO