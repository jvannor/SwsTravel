CREATE TABLE FacilityType(
	FacilityTypeID INT IDENTITY(1,1) NOT NULL,
	FacilityTypeName NVARCHAR(128) NOT NULL,
	FacilityDescription NVARCHAR(512),
	CONSTRAINT PK_FacilityType PRIMARY KEY (FacilityTypeID)
);
GO

CREATE TABLE Facility(
	FacilityID INT IDENTITY(1,1) NOT NULL,
	PartOfFacilityID INT,
	FacilityDescription NVARCHAR(512),
	FacilityName NVARCHAR(128) NOT NULL,
	FacilityTypeID INT NOT NULL,
	SquareFootage INT,
	CONSTRAINT PK_Facility PRIMARY KEY (FacilityID),
	CONSTRAINT FK_PartOfFacility FOREIGN KEY (PartOfFacilityID) 
	REFERENCES Facility(FacilityID),
	CONSTRAINT FK_FacilityTypeFacility FOREIGN KEY (FacilityTypeID)
	REFERENCES FacilityType(FacilityTypeID)
);
GO

CREATE TABLE TransportationFacility(
	FacilityID INT NOT NULL,
	CONSTRAINT PK_TransportationFacility PRIMARY KEY (FacilityID),
	CONSTRAINT FK_FacilityTransportationFacility FOREIGN KEY (FacilityID) 
	REFERENCES Facility(FacilityID)
);
GO

CREATE TABLE ShipPort(
	FacilityID INT NOT NULL,
	CONSTRAINT PK_ShipPort PRIMARY KEY (FacilityID),
	CONSTRAINT FK_TransportationFacilityShipPort FOREIGN KEY (FacilityID)
	REFERENCES TransportationFacility(FacilityID)
);
GO

CREATE TABLE TravelProduct(
    ProductID INT IDENTITY(1,1) NOT NULL,
    ProductName NVARCHAR(128) NOT NULL
	CONSTRAINT PK_TravelProduct PRIMARY KEY (ProductID)
);
GO

CREATE TABLE PassengerTransportationOffering(
	ProductID INT NOT NULL,
	FacilityIDGoingTo INT,
	FacilityIDOriginatingFrom INT,
	CONSTRAINT PK_PassengerTransportationOffering PRIMARY KEY (ProductID),
	CONSTRAINT FK_TravelProductPassengerTransportationOffering FOREIGN KEY (ProductID)
	REFERENCES TravelProduct(ProductID),
	CONSTRAINT FK_FacilityGoingToPassenterTransportationOffering FOREIGN KEY (FacilityIDGoingTo)
	REFERENCES Facility(FacilityID),
	CONSTRAINT FK_FacilityOriginatingFromTransportationOffering FOREIGN KEY (FacilityIDOriginatingFrom)
	REFERENCES Facility(FacilityID)
);
GO

CREATE TABLE ShipOffering(
	ProductID INT NOT NULL,
	CONSTRAINT PK_ShipOffering PRIMARY KEY (ProductID),
	CONSTRAINT FK_PassengerTransportationOfferingShipOffering FOREIGN KEY (ProductID)
	REFERENCES PassengerTransportationOffering(ProductID)
);
GO

CREATE TABLE ProductCategory(
	ProductCategoryID INT IDENTITY(1,1) NOT NULL,
	ProductCategoryDescrition NVARCHAR(512),
	CONSTRAINT PK_ProductCategory PRIMARY KEY (ProductCategoryID)
);
GO

CREATE TABLE ProductCategoryClassification(
	ProductCategoryID INT NOT NULL,
	ProductID INT NOT NULL,
	FromDate DATE NOT NULL,
	Comments NVARCHAR(512),
	PrimaryFlag BIT NOT NULL,
	ThruDate DATE NOT NULL,
	CONSTRAINT PK_ProductCategoryClassification PRIMARY KEY (ProductCategoryID, ProductID, FromDate),
	CONSTRAINT FK_ProductCategoryProductCategoryClassification FOREIGN KEY (ProductCategoryID)
	REFERENCES ProductCategory(ProductCategoryID),
	CONSTRAINT FK_TravelProductProductCategoryClassification FOREIGN KEY (ProductID)
	REFERENCES TravelProduct(ProductID)
);
GO