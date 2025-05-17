CREATE TABLE Users (
    Id BIGINT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing primary key
    Username NVARCHAR(100) NOT NULL,    -- Username with a max length of 100
    FirstName NVARCHAR(100) NOT NULL,   -- First name with a max length of 100
    LastName NVARCHAR(100) NOT NULL,    -- Last name with a max length of 100
    Email NVARCHAR(255) NOT NULL UNIQUE, -- Email with a max length of 255, must be unique
    Password NVARCHAR(MAX) NOT NULL     -- Password (hashed), no length restriction
);