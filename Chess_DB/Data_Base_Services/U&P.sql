-- CREATE TABLE Admins(user_name TEXT PRIMARY KEY,
--                      password_hash TEXT NOT NULL,
--                      email TEXT UNIQUE NOT NULL);

-- INSERT INTO Admins(user_name, password_hash, email)
-- VALUES('Adam', 'For now', 'HELP.PLS@Ecam.be');

-- DELETE FROM Admins WHERE admin_name = 'Adam';
SELECT sqlite_version() AS version;

INSERT INTO Admins(user, password_hash, email)
VALUES('Admin', 'qfde', 'jp');
ALTER TABLE Admins RENAME COLUMN admin_name TO user;

-- SELECT * FROM Admins;