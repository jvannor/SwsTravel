INSERT INTO FacilityType (FacilityTypeName, FacilityDescription)
VALUES ('Port', 'A town or city with a harbor where ships laod or unlead, especially one where customs officers are stationed');

DECLARE @FacilityTypeID INT
SELECT @FacilityTypeID = FacilityTypeID 
FROM FacilityType
WHERE FacilityTypeName = 'Port'

INSERT INTO Facility (FacilityName, FacilityTypeID)
VALUES 
('Nassau', @FacilityTypeID), 
('Miami', @FacilityTypeID)

DECLARE @NassauFacilityID INT
SELECT @NassauFacilityID = FacilityID
FROM Facility 
WHERE FacilityName = 'Nassau'

DECLARE @MiamiFacilityID INT
SELECT @MiamiFacilityID = FacilityID
FROM Facility
WHERE FacilityName = 'Miami'

INSERT INTO TransportationFacility (FacilityID)
VALUES
(@NassauFacilityID),
(@MiamiFacilityID)

INSERT INTO ShipPort (FacilityID)
VALUES
(@NassauFacilityID),
(@MiamiFacilityID)

INSERT INTO TravelProduct (ProductName)
VALUES ('Seven Day Carribean Cruise')

DECLARE @ProductID INT
SELECT @ProductID = ProductID 
FROM TravelProduct
WHERE ProductName = 'Seven Day Carribean Cruise'

INSERT INTO PassengerTransportationOffering (ProductID, FacilityIDOriginatingFrom, FacilityIDGoingTo)
VALUES (@ProductID, @MiamiFacilityID, @NassauFacilityID)

INSERT INTO ShipOffering (ProductID)
VALUES (@ProductID)