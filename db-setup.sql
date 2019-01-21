-- CREATE TABLE users (
--     id VARCHAR(255) NOT NULL,
--     username VARCHAR(20) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     hash VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),
--     UNIQUE KEY email (email)
-- );

-- CREATE TABLE Decks (
--     id int NOT NULL AUTO_INCREMENT,
--     name VARCHAR(20) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     saves INT DEFAULT 0,
--     shares INT DEFAULT 0,
--     colors VARCHAR(255),
--     userId VARCHAR(255),
--     gameFormat VARCHAR(255),
--     private TINYINT,
--     INDEX userId (userId),
--     FOREIGN KEY (userId)
--         REFERENCES users(id)
--         ON DELETE CASCADE,  
--     PRIMARY KEY (id)
-- );

-- CREATE TABLE Cards (
--     id INT NOT NULL AUTO_INCREMENT,
--     name VARCHAR(20) NOT NULL,
--     text VARCHAR(255) NOT NULL,
--     imgUrl VARCHAR(255),
--     uses INT DEFAULT 0,
--     PRIMARY KEY (id)
-- );
-- CREATE TABLE deckcards (
--     id int NOT NULL AUTO_INCREMENT,
--     deckId INT NOT NULL,
--     cardId INT NOT NULL,
--     userId VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id),
--     INDEX (deckId, cardId),
--     INDEX (userId),

--     FOREIGN KEY (userId)
--         REFERENCES users(id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (deckId)
--         REFERENCES decks(id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (cardId)
--         REFERENCES Cards(id)
--         ON DELETE CASCADE
-- )


-- USE THIS LINE FOR GET KEEPS BY VAULTID
-- SELECT * FROM deckcards dc
-- INNER JOIN cards c ON c.id = dc.cardId 
-- WHERE (deckId = @deckId AND dc.userId = @userId) 
