CREATE OR REPLACE PROCEDURE Event_Insert 
(
	P_EventCode 		VARCHAR(20),
	P_Name 				VARCHAR(50),
	P_Description 		VARCHAR(200),
	P_EventDate 		TIMESTAMP,
	P_Venue 			VARCHAR(50),
	P_TotalTickets 		INT,
	P_AvailableTickets 	INT,
	P_BasePrice 		DECIMAL(18,2),
	P_CreatedAt 		TIMESTAMP,
	P_IsActive 			BOOL,
	NextId				INOUT INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO EVENT
	(
		EventCode,
		Name,
		Description,
		EventDate,
		Venue,
		TotalTickets,
		AvailableTickets,
		BasePrice,
		CreatedAt,
		IsActive
	)
	VALUES
	(
		P_EventCode,
		P_Name,
		P_Description,
		P_EventDate,
		P_Venue,
		P_TotalTickets,
		P_AvailableTickets,
		P_BasePrice,
		P_CreatedAt,
		P_IsActive
	)
	RETURNING Id INTO NextId;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE Event_Update
(
	P_Id 				INT,
	P_EventCode 		VARCHAR(20),
	P_Name 				VARCHAR(50),
	P_Description 		VARCHAR(200),
	P_EventDate 		TIMESTAMP,
	P_Venue 			VARCHAR(50),
	P_TotalTickets 		INT,
	P_AvailableTickets 	INT,
	P_BasePrice 		DECIMAL(18,2),
	P_IsActive 			BOOL
)
LANGUAGE plpgsql
AS $$
BEGIN
	UPDATE	EVENT
	SET		EventCode = P_EventCode,
			Name = P_Name,
			Description = P_Description,
			EventDate = P_EventDate,
			Venue = P_Venue,
			TotalTickets = P_TotalTickets,
			AvailableTickets = P_AvailableTickets,
			BasePrice = P_BasePrice,
			IsActive = P_IsActive
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE Event_DeleteById
(
	P_Id	INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	DELETE
	FROM	EVENT
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;
CREATE OR REPLACE PROCEDURE Reservation_Insert 
(
	P_ReservationCode 	VARCHAR(20),
	P_UserId 			INT,
	P_EventId 			INT,
	P_SalesChannelId 	INT,
	P_TicketQuantity 	INT,
	P_TotalAmount 		DECIMAL(18,2),
	P_Status 			INT,
	P_ReservedAt 		TIMESTAMP,
	NextId				INOUT INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO RESERVATION
	(
		ReservationCode,
		UserId,
		EventId,
		TicketQuantity,
		TotalAmount,
		Status,
		SalesChannelId,
		ReservedAt
	)
	VALUES
	(
		P_ReservationCode,
		P_UserId,
		P_EventId,
		P_TicketQuantity,
		P_TotalAmount,
		P_Status,
		P_SalesChannelId,
		P_ReservedAt
	)
	RETURNING Id INTO NextId;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE Reservation_Update
(
	P_Id				INT,
	P_ReservationCode 	VARCHAR(20),
	P_UserId 			INT,
	P_EventId 			INT,
	P_SalesChannelId 	INT,
	P_TicketQuantity 	INT,
	P_TotalAmount 		DECIMAL(18,2),
	P_Status 			INT,
	P_ExpiresAt 		TIMESTAMP,
	P_ConfirmedAt 		TIMESTAMP
)
LANGUAGE plpgsql
AS $$
BEGIN
	UPDATE	RESERVATION
	SET		ReservationCode = P_ReservationCode,
			UserId = P_UserId,
			EventId = P_EventId,
			TicketQuantity = P_TicketQuantity,
			TotalAmount = P_TotalAmount,
			Status = P_Status,
			SalesChannelId = P_SalesChannelId,
			ExpiresAt = P_ExpiresAt,
			ConfirmedAt = P_ConfirmedAt
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE Reservation_DeleteById
(
	P_Id	INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	DELETE
	FROM	RESERVATION
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;
CREATE OR REPLACE PROCEDURE SalesChannel_Insert 
(
	P_ChannelCode 	VARCHAR(20),
	P_Name 			VARCHAR(50),
	P_Type 			INT,
	P_ApiKey 		VARCHAR(50),
	P_IsActive 		BOOL,
	P_CreatedAt 	TIMESTAMP,
	NextId			INOUT INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO SALESCHANNEL
	(
		ChannelCode,
		Name,
		Type,
		ApiKey,
		IsActive,
		CreatedAt
	)
	VALUES
	(
		P_ChannelCode,
		P_Name,
		P_Type,
		P_ApiKey,
		P_IsActive,
		P_CreatedAt
	)
	RETURNING Id INTO NextId;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE SalesChannel_Update
(
	P_Id 			INT,
	P_ChannelCode 	VARCHAR(20),
	P_Name 			VARCHAR(50),
	P_Type 			INT,
	P_ApiKey 		VARCHAR(50),
	P_IsActive 		BOOL
)
LANGUAGE plpgsql
AS $$
BEGIN
	UPDATE	SALESCHANNEL
	SET		ChannelCode = P_ChannelCode,
			Name = P_Name,
			Type = P_Type,
			ApiKey = P_ApiKey,
			IsActive = P_IsActive
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE SalesChannel_DeleteById
(
	P_Id	INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	DELETE
	FROM	SALESCHANNEL
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;
CREATE OR REPLACE PROCEDURE Ticket_Insert 
(
	P_TicketCode	 	VARCHAR(20),
	P_EventId	 		INT,
	P_ReservationId	 	INT,
	P_Status	 		INT,
	P_Price	 			DECIMAL(18,2),
	P_SeatNumber	 	VARCHAR(20),
	P_CreatedAt	 		TIMESTAMP WITHOUT TIME ZONE,
	NextId				INOUT INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO TICKET
	(
		TicketCode,
		EventId,
		ReservationId,
		Status,
		Price,
		SeatNumber,
		CreatedAt
	)
	VALUES
	(
		P_TicketCode,
		P_EventId,
		P_ReservationId,
		P_Status,
		P_Price,
		P_SeatNumber,
		P_CreatedAt
	)
	RETURNING Id INTO NextId;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE Ticket_Update
(
	P_Id				INT,
	P_TicketCode	 	VARCHAR(20),
	P_EventId	 		INT,
	P_ReservationId	 	INT,
	P_Status	 		INT,
	P_Price	 			DECIMAL(18,2),
	P_SeatNumber	 	VARCHAR(20)
)
LANGUAGE plpgsql
AS $$
BEGIN
	UPDATE	TICKET
	SET		TicketCode = P_TicketCode,
			EventId = P_EventId,
			ReservationId = P_ReservationId,
			Status = P_Status,
			Price = P_Price,
			SeatNumber = P_SeatNumber
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE Ticket_DeleteById
(
	P_Id	INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	DELETE
	FROM	TICKET
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;
CREATE OR REPLACE PROCEDURE User_Insert 
(
	P_UserCode 		VARCHAR(20),
	P_Name 			VARCHAR(100),
	P_Email 		VARCHAR(100),
	P_PhoneNumber 	VARCHAR(20),
	P_CreatedAt 	TIMESTAMP,
	P_IsActive 		BOOL,
	NextId			INOUT INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	INSERT INTO APPUSER
	(
		UserCode,
		Name,
		Email,
		PhoneNumber,
		CreatedAt,
		IsActive
	)
	VALUES
	(
		P_UserCode,
		P_Name,
		P_Email,
		P_PhoneNumber,
		P_CreatedAt,
		P_IsActive
	)
	RETURNING Id INTO NextId;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE User_Update
(
	P_Id 			INT,
	P_UserCode 		VARCHAR(20),
	P_Name 			VARCHAR(100),
	P_Email 		VARCHAR(100),
	P_PhoneNumber 	VARCHAR(20),
	P_IsActive 		BOOL
)
LANGUAGE plpgsql
AS $$
BEGIN
	UPDATE	APPUSER
	SET		UserCode = P_UserCode,
			Name = P_Name,
			Email = P_Email,
			PhoneNumber = P_PhoneNumber,
			IsActive = P_IsActive
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;

CREATE OR REPLACE PROCEDURE User_DeleteById
(
	P_Id	INT
)
LANGUAGE plpgsql
AS $$
BEGIN
	DELETE
	FROM	APPUSER
	WHERE	Id = P_Id;
	COMMIT;
END;
$$;
