DROP FUNCTION Event_ExistsById;
DROP FUNCTION Event_GetById;
DROP FUNCTION Events_GetAll;
DROP FUNCTION Events_GetAll_SortByCreatedAt;

--Event

CREATE OR REPLACE FUNCTION Event_ExistsById
(
	P_Id INT
)
RETURNS TABLE (
				ReturnValue INT
			  )
LANGUAGE plpgsql
AS $$
DECLARE V_NumeroRegistros INT;
BEGIN
	V_NumeroRegistros := 0;
	
	SELECT 	COUNT(Id)
	INTO	V_NumeroRegistros
	FROM	EVENT
	WHERE 	Id = P_Id;
	
	IF V_NumeroRegistros > 0 THEN
		RETURN QUERY
		SELECT 1;
	ELSE
		RETURN QUERY
		SELECT 0;
	END IF;
END;
$$;

CREATE OR REPLACE FUNCTION Events_GetAll()
	RETURNS TABLE (
					Id 					INT,
					EventCode 			VARCHAR(20),
					Name 				VARCHAR(50),
					Description 		VARCHAR(200),
					EventDate 			TIMESTAMP,
					Venue 				VARCHAR(50),
					TotalTickets 		INT,
					AvailableTickets 	INT,
					BasePrice 			DECIMAL(18,2),
					CreatedAt 			TIMESTAMP,
					IsActive 			BOOL
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.EventCode,
			T.Name,
			T.Description,
			T.EventDate,
			T.Venue,
			T.TotalTickets,
			T.AvailableTickets,
			T.BasePrice,
			T.CreatedAt,
			T.IsActive
	FROM	EVENT T;
END;
$$;

CREATE OR REPLACE FUNCTION Events_GetAll_SortByCreatedAt()
	RETURNS TABLE (
					Id 					INT,
					EventCode 			VARCHAR(20),
					Name 				VARCHAR(50),
					Description 		VARCHAR(200),
					EventDate 			TIMESTAMP,
					Venue 				VARCHAR(50),
					TotalTickets 		INT,
					AvailableTickets 	INT,
					BasePrice 			DECIMAL(18,2),
					CreatedAt 			TIMESTAMP,
					IsActive 			BOOL
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT		T.Id,
				T.EventCode,
				T.Name,
				T.Description,
				T.EventDate,
				T.Venue,
				T.TotalTickets,
				T.AvailableTickets,
				T.BasePrice,
				T.CreatedAt,
				T.IsActive
	FROM		EVENT T
	ORDER BY 	T.CreatedAt DESC;
END;
$$;

CREATE OR REPLACE FUNCTION Event_GetById
(
	P_Id INT
)
	RETURNS TABLE (
					Id 					INT,
					EventCode 			VARCHAR(20),
					Name 				VARCHAR(50),
					Description 		VARCHAR(200),
					EventDate 			TIMESTAMP,
					Venue 				VARCHAR(50),
					TotalTickets 		INT,
					AvailableTickets 	INT,
					BasePrice 			DECIMAL(18,2),
					CreatedAt 			TIMESTAMP,
					IsActive 			BOOL
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.EventCode,
			T.Name,
			T.Description,
			T.EventDate,
			T.Venue,
			T.TotalTickets,
			T.AvailableTickets,
			T.BasePrice,
			T.CreatedAt,
			T.IsActive
	FROM	EVENT T
	WHERE	T.Id = P_Id;
END;
$$;

-- Reservation

DROP FUNCTION Reservation_ExistsById;
DROP FUNCTION Reservation_GetById;
DROP FUNCTION Reservations_GetAll;
DROP FUNCTION Reservations_GetAll_SortByReservedAt;

CREATE OR REPLACE FUNCTION Reservation_ExistsById
(
	P_Id INT
)
RETURNS TABLE (
				ReturnValue INT
			  )
LANGUAGE plpgsql
AS $$
DECLARE V_NumeroRegistros INT;
BEGIN
	V_NumeroRegistros := 0;
	
	SELECT 	COUNT(Id)
	INTO	V_NumeroRegistros
	FROM	RESERVATION
	WHERE 	Id = P_Id;
	
	IF V_NumeroRegistros > 0 THEN
		RETURN QUERY
		SELECT 1;
	ELSE
		RETURN QUERY
		SELECT 0;
	END IF;
END;
$$;

CREATE OR REPLACE FUNCTION Reservations_GetAll()
	RETURNS TABLE (
					Id 				INT,
					ReservationCode VARCHAR(20),
					UserId 			INT,
					EventId 		INT,
					SalesChannelId 	INT,
					TicketQuantity 	INT,
					TotalAmount 	DECIMAL(18,2),
					Status 			INT,
					ReservedAt 		TIMESTAMP,
					ExpiresAt 		TIMESTAMP,
					ConfirmedAt 	TIMESTAMP
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.ReservationCode,
			T.UserId,
			T.EventId,
			T.SalesChannelId,
			T.TicketQuantity,
			T.TotalAmount,
			T.Status,
			T.ReservedAt,
			T.ExpiresAt,
			T.ConfirmedAt
	FROM	RESERVATION T;
END;
$$;

CREATE OR REPLACE FUNCTION Reservations_GetAll_SortByReservedAt()
	RETURNS TABLE (
					Id 					INT,
					ReservationCode 	VARCHAR(20),
					UserId 				INT,
					UserName			VARCHAR(100),
					EventId 			INT,
					EventName			VARCHAR(50),
					SalesChannelId 		INT,
					SalesChannelName	VARCHAR(50),
					TicketQuantity 		INT,
					TotalAmount 		DECIMAL(18,2),
					Status 				INT,
					ReservedAt 			TIMESTAMP,
					ExpiresAt 			TIMESTAMP,
					ConfirmedAt 		TIMESTAMP
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT		R.Id,
				R.ReservationCode,
				R.UserId,
				U.Name AS UserName,
				R.EventId,
				E.Name AS EventName,
				R.SalesChannelId,
				S.Name AS SalesChannelName,
				R.TicketQuantity,
				R.TotalAmount,
				R.Status,
				R.ReservedAt,
				R.ExpiresAt,
				R.ConfirmedAt
	FROM		RESERVATION R
	INNER JOIN	APPUSER U ON R.UserId = U.Id
	INNER JOIN	EVENT E ON R.EventId = E.Id
	INNER JOIN	SALESCHANNEL S ON R.SalesChannelId = S.Id
	ORDER BY	R.ReservedAt DESC;
END;
$$;

CREATE OR REPLACE FUNCTION Reservation_GetById
(
	P_Id INT
)
	RETURNS TABLE (
					Id 				INT,
					ReservationCode VARCHAR(20),
					UserId 			INT,
					EventId 		INT,
					SalesChannelId 	INT,
					TicketQuantity 	INT,
					TotalAmount 	DECIMAL(18,2),
					Status 			INT,
					ReservedAt 		TIMESTAMP,
					ExpiresAt 		TIMESTAMP,
					ConfirmedAt 	TIMESTAMP
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.ReservationCode,
			T.UserId,
			T.EventId,
			T.SalesChannelId,
			T.TicketQuantity,
			T.TotalAmount,
			T.Status,
			T.ReservedAt,
			T.ExpiresAt,
			T.ConfirmedAt
	FROM	RESERVATION T
	WHERE	T.Id = P_Id;
END;
$$;

-- SalesChannel

DROP FUNCTION SalesChannel_ExistsById;
DROP FUNCTION SalesChannel_GetById;
DROP FUNCTION SalesChannels_GetAll;
DROP FUNCTION SalesChannels_GetAll_SortByCreatedAt;

CREATE OR REPLACE FUNCTION SalesChannel_ExistsById
(
	P_Id INT
)
RETURNS TABLE (
				ReturnValue INT
			  )
LANGUAGE plpgsql
AS $$
DECLARE V_NumeroRegistros INT;
BEGIN
	V_NumeroRegistros := 0;
	
	SELECT 	COUNT(Id)
	INTO	V_NumeroRegistros
	FROM	SALESCHANNEL
	WHERE 	Id = P_Id;
	
	IF V_NumeroRegistros > 0 THEN
		RETURN QUERY
		SELECT 1;
	ELSE
		RETURN QUERY
		SELECT 0;
	END IF;
END;
$$;

CREATE OR REPLACE FUNCTION SalesChannels_GetAll()
	RETURNS TABLE (
					Id 			INT,
					ChannelCode VARCHAR(20),
					Name 		VARCHAR(50),
					Type 		INT,
					ApiKey 		VARCHAR(50),
					IsActive 	BOOL,
					CreatedAt 	TIMESTAMP
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.ChannelCode,
			T.Name,
			T.Type,
			T.ApiKey,
			T.IsActive,
			T.CreatedAt
	FROM	SALESCHANNEL T;
END;
$$;

CREATE OR REPLACE FUNCTION SalesChannels_GetAll_SortByCreatedAt()
	RETURNS TABLE (
					Id 			INT,
					ChannelCode VARCHAR(20),
					Name 		VARCHAR(50),
					Type 		INT,
					ApiKey 		VARCHAR(50),
					IsActive 	BOOL,
					CreatedAt 	TIMESTAMP
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT		T.Id,
				T.ChannelCode,
				T.Name,
				T.Type,
				T.ApiKey,
				T.IsActive,
				T.CreatedAt
	FROM		SALESCHANNEL T
	ORDER BY	T.CreatedAt DESC;
END;
$$;

CREATE OR REPLACE FUNCTION SalesChannel_GetById
(
	P_Id INT
)
	RETURNS TABLE (
					Id 			INT,
					ChannelCode VARCHAR(20),
					Name 		VARCHAR(50),
					Type 		INT,
					ApiKey 		VARCHAR(50),
					IsActive 	BOOL,
					CreatedAt 	TIMESTAMP
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.ChannelCode,
			T.Name,
			T.Type,
			T.ApiKey,
			T.IsActive,
			T.CreatedAt
	FROM	SALESCHANNEL T
	WHERE	T.Id = P_Id;
END;
$$;

-- Ticket

DROP FUNCTION Ticket_ExistsById;
DROP FUNCTION Ticket_GetById;
DROP FUNCTION Tickets_GetAll;
DROP FUNCTION Tickets_GetAll_SortByCreatedAt;

CREATE OR REPLACE FUNCTION Ticket_ExistsById
(
	P_Id INT
)
RETURNS TABLE (
				ReturnValue INT
			  )
LANGUAGE plpgsql
AS $$
DECLARE V_NumeroRegistros INT;
BEGIN
	V_NumeroRegistros := 0;
	
	SELECT 	COUNT(Id)
	INTO	V_NumeroRegistros
	FROM	TICKET
	WHERE 	Id = P_Id;
	
	IF V_NumeroRegistros > 0 THEN
		RETURN QUERY
		SELECT 1;
	ELSE
		RETURN QUERY
		SELECT 0;
	END IF;
END;
$$;

CREATE OR REPLACE FUNCTION Tickets_GetAll()
	RETURNS TABLE (
					Id 				INT,
					TicketCode 		VARCHAR(20),
					EventId 		INT,
					ReservationId 	INT,
					Status 			INT,
					Price 			DECIMAL,
					SeatNumber 		VARCHAR,
					CreatedAt 		TIMESTAMP WITHOUT TIME ZONE
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.TicketCode,
			T.EventId,
			T.ReservationId,
			T.Status,
			T.Price,
			T.SeatNumber,
			T.CreatedAt
	FROM	TICKET T;
END;
$$;

CREATE OR REPLACE FUNCTION Tickets_GetAll_SortByCreatedAt()
	RETURNS TABLE (
					Id 				INT,
					TicketCode 		VARCHAR(20),
					EventId 		INT,
					EventName		VARCHAR(50),
					ReservationId 	INT,
					ReservationCode	VARCHAR(20),
					Status 			INT,
					Price 			DECIMAL,
					SeatNumber 		VARCHAR,
					CreatedAt 		TIMESTAMP WITHOUT TIME ZONE
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT		T.Id,
				T.TicketCode,
				T.EventId,
				E.Name AS EventName,
				T.ReservationId,
				R.ReservationCode,
				T.Status,
				T.Price,
				T.SeatNumber,
				T.CreatedAt
	FROM		TICKET T
	INNER JOIN	Event E ON T.EventId = E.Id
	INNER JOIN	Reservation R ON T.ReservationId = R.Id
	ORDER BY 	T.CreatedAt DESC;
END;
$$;

CREATE OR REPLACE FUNCTION Ticket_GetById
(
	P_Id INT
)
	RETURNS TABLE (
					Id 				INT,
					TicketCode 		VARCHAR(20),
					EventId 		INT,
					ReservationId 	INT,
					Status 			INT,
					Price 			DECIMAL,
					SeatNumber 		VARCHAR,
					CreatedAt 		TIMESTAMP WITHOUT TIME ZONE
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.TicketCode,
			T.EventId,
			T.ReservationId,
			T.Status,
			T.Price,
			T.SeatNumber,
			T.CreatedAt
	FROM	TICKET T
	WHERE	T.Id = P_Id;
END;
$$;

-- User

DROP FUNCTION User_ExistsById;
DROP FUNCTION User_GetById;
DROP FUNCTION Users_GetAll;
DROP FUNCTION Users_GetAll_SortByCreatedAt;

CREATE OR REPLACE FUNCTION User_ExistsById
(
	P_Id INT
)
RETURNS TABLE (
				ReturnValue INT
			  )
LANGUAGE plpgsql
AS $$
DECLARE V_NumeroRegistros INT;
BEGIN
	V_NumeroRegistros := 0;
	
	SELECT 	COUNT(Id)
	INTO	V_NumeroRegistros
	FROM	APPUSER
	WHERE 	Id = P_Id;
	
	IF V_NumeroRegistros > 0 THEN
		RETURN QUERY
		SELECT 1;
	ELSE
		RETURN QUERY
		SELECT 0;
	END IF;
END;
$$;

CREATE OR REPLACE FUNCTION Users_GetAll()
	RETURNS TABLE (
					Id 				INT,
					UserCode 		VARCHAR(20),
					Name 			VARCHAR(100),
					Email 			VARCHAR(100),
					PhoneNumber 	VARCHAR(20),
					CreatedAt 		TIMESTAMP,
					IsActive 		BOOL
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.UserCode,
			T.Name,
			T.Email,
			T.PhoneNumber,
			T.CreatedAt,
			T.IsActive
	FROM	APPUSER T;
END;
$$;

CREATE OR REPLACE FUNCTION Users_GetAll_SortByCreatedAt()
	RETURNS TABLE (
					Id 				INT,
					UserCode 		VARCHAR(20),
					Name 			VARCHAR(100),
					Email 			VARCHAR(100),
					PhoneNumber 	VARCHAR(20),
					CreatedAt 		TIMESTAMP,
					IsActive 		BOOL
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT		T.Id,
				T.UserCode,
				T.Name,
				T.Email,
				T.PhoneNumber,
				T.CreatedAt,
				T.IsActive
	FROM		APPUSER T
	ORDER BY 	T.CreatedAt DESC;
END;
$$;

CREATE OR REPLACE FUNCTION User_GetById
(
	P_Id INT
)
	RETURNS TABLE (
					Id 				INT,
					UserCode 		VARCHAR(20),
					Name 			VARCHAR(100),
					Email 			VARCHAR(100),
					PhoneNumber 	VARCHAR(20),
					CreatedAt 		TIMESTAMP,
					IsActive 		BOOL
				  )
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT	T.Id,
			T.UserCode,
			T.Name,
			T.Email,
			T.PhoneNumber,
			T.CreatedAt,
			T.IsActive
	FROM	APPUSER T
	WHERE	T.Id = P_Id;
END;
$$;