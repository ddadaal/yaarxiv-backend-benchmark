-- MySQL dump 10.13  Distrib 8.0.21, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: yaarxiv_dev
-- ------------------------------------------------------
-- Server version	8.0.21

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `article`
--

DROP TABLE IF EXISTS `article`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `article` (
  `id` int NOT NULL AUTO_INCREMENT,
  `createTime` datetime NOT NULL,
  `lastUpdateTime` datetime NOT NULL,
  `latestRevisionNumber` int NOT NULL,
  `ownerId` varchar(255) NOT NULL,
  `ownerSetPublicity` tinyint(1) NOT NULL DEFAULT (true),
  `adminSetPublicity` tinyint(1) NOT NULL DEFAULT (true),
  PRIMARY KEY (`id`),
  KEY `FK_9c7bd5faae7271b4f09dc64a165` (`ownerId`),
  CONSTRAINT `FK_9c7bd5faae7271b4f09dc64a165` FOREIGN KEY (`ownerId`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `article`
--

LOCK TABLES `article` WRITE;
/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article` (`id`, `createTime`, `lastUpdateTime`, `latestRevisionNumber`, `ownerId`, `ownerSetPublicity`, `adminSetPublicity`) VALUES (2,'2020-09-25 16:33:38','2020-09-25 16:33:38',2,'pHk3ZAm7C6kw',1,1);
/*!40000 ALTER TABLE `article` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `article_revision`
--

DROP TABLE IF EXISTS `article_revision`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `article_revision` (
  `id` int NOT NULL AUTO_INCREMENT,
  `revisionNumber` int NOT NULL,
  `time` datetime NOT NULL,
  `title` varchar(255) NOT NULL,
  `authors` text NOT NULL,
  `keywords` text NOT NULL,
  `abstract` varchar(500) NOT NULL,
  `category` varchar(255) NOT NULL,
  `articleId` int NOT NULL,
  `pdfId` varchar(36) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_7e607abc0262a18c67b2fba37b2` (`pdfId`),
  KEY `FK_39812c68ea9557fc09d647743ae` (`articleId`),
  CONSTRAINT `FK_39812c68ea9557fc09d647743ae` FOREIGN KEY (`articleId`) REFERENCES `article` (`id`) ON DELETE CASCADE,
  CONSTRAINT `FK_7e607abc0262a18c67b2fba37b2` FOREIGN KEY (`pdfId`) REFERENCES `pdf_upload` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `article_revision`
--

LOCK TABLES `article_revision` WRITE;
/*!40000 ALTER TABLE `article_revision` DISABLE KEYS */;
INSERT INTO `article_revision` (`id`, `revisionNumber`, `time`, `title`, `authors`, `keywords`, `abstract`, `category`, `articleId`, `pdfId`) VALUES (2,1,'2020-09-25 16:33:38','1','[{\"name\":\"1\"}]','1','1','',2,'4f05500f-6f48-486b-940a-61c2e9399b7c'),(3,2,'2020-09-25 17:37:31','14','[{\"name\":\"1\"}]','1','1','',2,'4f05500f-6f48-486b-940a-61c2e9399b7c');
/*!40000 ALTER TABLE `article_revision` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pdf_upload`
--

DROP TABLE IF EXISTS `pdf_upload`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pdf_upload` (
  `id` varchar(36) NOT NULL,
  `userId` varchar(255) NOT NULL,
  `link` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_d726da16923089065da0e46afb9` (`userId`),
  CONSTRAINT `FK_d726da16923089065da0e46afb9` FOREIGN KEY (`userId`) REFERENCES `user` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pdf_upload`
--

LOCK TABLES `pdf_upload` WRITE;
/*!40000 ALTER TABLE `pdf_upload` DISABLE KEYS */;
INSERT INTO `pdf_upload` (`id`, `userId`, `link`) VALUES ('4f05500f-6f48-486b-940a-61c2e9399b7c','pHk3ZAm7C6kw','pHk3ZAm7C6kw/1601022818315_全球付(5319480002004223).pdf'),('c463f475-30f0-4069-adb9-c32a6e951f3a','0OuTyAxU1VwS','0OuTyAxU1VwS/1600653835282_1.pdf');
/*!40000 ALTER TABLE `pdf_upload` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reset_password_token`
--

DROP TABLE IF EXISTS `reset_password_token`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reset_password_token` (
  `id` varchar(255) NOT NULL,
  `userEmail` varchar(255) NOT NULL,
  `time` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reset_password_token`
--

LOCK TABLES `reset_password_token` WRITE;
/*!40000 ALTER TABLE `reset_password_token` DISABLE KEYS */;
/*!40000 ALTER TABLE `reset_password_token` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `role` enum('user','admin') NOT NULL DEFAULT 'user',
  PRIMARY KEY (`id`),
  UNIQUE KEY `IDX_e12875dfb3b1d92d7d7c5377e2` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`id`, `name`, `password`, `email`, `role`) VALUES ('0OuTyAxU1VwS','test','$2a$10$wb2DUjDExDBWvGEOLt2lpus4wp4/y5PXzKEpgrpH3r9rilRmYHaai','test@test.com','admin'),('OxXJ654ta2yV','2','$2a$10$a6/wBTrjS7x.xrLbFjtMFOv1DiDxvtvmg2SqxpVbLJuH4m1oaE7yW','2@2.com','user'),('pHk3ZAm7C6kw','1','$2a$10$Z1tEq8ssDA9LxXGhWWjlgOKS1qv3ZN9/q2JDu2SPsbI0N4htokKL.','1@1.com','user');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-09-26 20:06:51
