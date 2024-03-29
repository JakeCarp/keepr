CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT "Last Update",
  name TEXT NOT NULL,
  description TEXT,
  img TEXT NOT NULL,
  views INT DEFAULT 0,
  shares INT DEFAULT 0,
  keeps INT DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: creator ID',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;
CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: creator ID',
  name TEXT NOT NULL,
  description TEXT,
  isPrivate TINYINT,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8;
CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT 'primary key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: creator ID',
  vaultId INT NOT NULL COMMENT 'FK: vault ID',
  keepId INT NOT NULL COMMENT 'FK: keep ID',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8;
DELETE FROM
  vaults;