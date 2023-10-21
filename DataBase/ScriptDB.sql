DROP DATABASE intersectmessages;
CREATE DATABASE intersectmessages;
USE intersectmessages;

CREATE TABLE`satelite`(
`SateliteId` INT NOT NULL AUTO_INCREMENT, 
`SateliteName` VARCHAR(255)  NULL,
`Coordenadax` VARCHAR(255)  NULL,
`Coordenaday` VARCHAR(255)  NULL,
`AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
PRIMARY KEY (`SateliteId`) ) ENGINE = InnoDB;

CREATE TABLE `messagesIntersect` (
    `MessageId` INT NOT NULL AUTO_INCREMENT,
    `SateliteIdRef` INT,
    `Consecutive` INT,
    `Distance` decimal,
    `Message` VARCHAR(255) NULL,
    `AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (`MessageId`),UNIQUE(MessageId),
    FOREIGN KEY (`SateliteIdRef`) REFERENCES `satelite`(`SateliteId`)
) ENGINE = InnoDB;

CREATE TABLE `decryptedMessage` (
    `decryptedMessageId` INT NOT NULL AUTO_INCREMENT,
    `Consecutive` INT,
    `Coordenadax` VARCHAR(255)  NULL,
	`Coordenaday` VARCHAR(255)  NULL,
    `Message` VARCHAR(255) NULL,
    `AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (`decryptedMessageId`),UNIQUE(MessageId)
  
) ENGINE = InnoDB;
