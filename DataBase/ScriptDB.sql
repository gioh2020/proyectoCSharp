DROP DATABASE intersectmessages;
CREATE DATABASE intersectmessages;
USE intersectmessages;

CREATE TABLE`satelite`(
`SateliteId` INT NOT NULL AUTO_INCREMENT, 
`SateliteName` VARCHAR(255)  NULL,
`CoordinateX` VARCHAR(255)  NULL,
`CoordinateY` VARCHAR(255)  NULL,
`AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
PRIMARY KEY (`SateliteId`) ) ENGINE = InnoDB;

CREATE TABLE `messagesIntersect` (
    `MessageId` INT NOT NULL AUTO_INCREMENT,
    `SateliteIdRef` INT NOT NULL,
    `Consecutive` INT NOT NULL,
    `Distance` DECIMAL(12 , 2) NOT NULL,
    `Message` VARCHAR(255) NULL,
    `AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (`MessageId`),UNIQUE(MessageId),
    FOREIGN KEY (`SateliteIdRef`) REFERENCES `satelite`(`SateliteId`)
) ENGINE = InnoDB;

CREATE TABLE `decryptedMessage` (
    `decryptedMessageId` INT NOT NULL AUTO_INCREMENT,
    `Consecutive` INT,
    `CoordinateX` DECIMAL  NULL,
	`CoordinateY` DECIMAL  NULL,
    `Message` VARCHAR(255) NULL,
    `AuditDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    PRIMARY KEY (`decryptedMessageId`),UNIQUE(`Consecutive`)
  
) ENGINE = InnoDB;

