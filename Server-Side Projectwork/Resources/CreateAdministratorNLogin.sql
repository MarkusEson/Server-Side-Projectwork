﻿DROP TABLE ADMINISTRATOR
GO

CREATE TABLE ADMINISTRATOR(
   AdminId INT NOT NULL IDENTITY(0,1),
   AdminRank INT NOT NULL,
   UserName VARCHAR(20) UNIQUE NOT NULL,
   PassSalt VARCHAR(128) NOT NULL,
   PassHash VARCHAR(128) NOT NULL,
   FirstName VARCHAR(25) NULL,
   LastName VARCHAR(25) NULL,
   AdminDesc TEXT NULL,
   PRIMARY KEY ( AdminId ),
);
GO