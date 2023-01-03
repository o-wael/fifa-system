Create Database fifaDB;
Use fifaDB;

Insert Into SystemUser
Values ('admin', 'admin')
Insert Into SystemAdmin
Values ('admin', 'Hamza Namira')

Go;

Create Procedure createAllTables AS

Create Table SystemUser(
    username varchar(20),
    password varchar(20),
    Constraint PK_SystemUser Primary Key(username)
);

Create Table SystemAdmin(
    id int Identity,
    username varchar(20) Not Null,
    name varchar(20),
    Constraint PK_SystemAdmin Primary Key(id),
    Constraint FK_SystemAdmin Foreign Key(username) 
        references SystemUser(username) on update cascade,
    Constraint UK_SystemAdmin unique(username)
);

Create Table SportsAssociationManager(
    id int Identity,
    username varchar(20) Not Null,
    name varchar(20),
    Constraint PK_SportsAssociationManager Primary Key(id),
    Constraint FK_SportsAssociationManager Foreign Key(username) 
        references SystemUser(username) on update cascade,
    Constraint UK_SportsAssociationManager unique(username) 
);

Create Table Fan(
    username varchar(20) Not Null,
    national_id varchar(20),
    name varchar(20),
    address varchar(20),
    phone_number bigint,
    status bit Default '1',
    birth_date Date,
    Constraint PK_Fan Primary Key(national_id),
    Constraint FK_Fan Foreign Key(username)
        references SystemUser(username) on update cascade,
    Constraint UK_Fan unique(username) 
);

Create Table Stadium(
    id int identity,
    location varchar(20),
    capacity int,
    name varchar(20) Unique,
    status bit Default '1', 
    Constraint PK_Stadium Primary Key (id)
);

Create Table Club(
    id int identity,
    name varchar(20) Unique,
    location varchar(20), 
    Constraint PK_Club Primary Key(id)
);

Create Table Manager(
    id int Identity,
    username varchar(20) Not Null,
    name varchar(20),
    stadium_id int,
    Constraint PK_Manager Primary Key(id),
    Constraint FK_Manager_Stadium Foreign Key(stadium_id) 
        references Stadium(id) on update cascade on delete cascade,
    Constraint FK_Manager_SystemUser Foreign Key(username) 
        references SystemUser(username) on update cascade on delete cascade,
    Constraint UK_Manager unique(username) 
);

Create Table Representative(
    id int Identity,
    username varchar(20) Not Null,
    name varchar(20),
    club_id int,
    Constraint PK_Representative Primary Key(id),
    Constraint FK_Representative_Club Foreign Key(club_id)
        references Club(id) on update cascade on delete cascade,
    Constraint FK_Representative_SystemUser Foreign Key(username) 
        references SystemUser(username) on update cascade on delete cascade,
    Constraint UK_Representative unique(username) 
);

Create Table Host_Request(
    id int identity,
    status varchar(20) Default 'Unhandled',
    match_id int,
    manager_id int,
    representative_id int,
    Constraint PK_Host_Request Primary Key(id),
    Constraint FK_HostRequest_Manager Foreign Key (manager_id)
        references Manager(id) on update cascade on delete cascade,
    Constraint FK_HostRequest_Representative Foreign Key (representative_id)
        references Representative(id)
);

Create Table Match(
    id int identity,
    start_time DateTime,
    end_time DateTime,
    stadium_id int,
    Constraint PK_Match Primary Key(id),
    Constraint FK_Match_Stadium Foreign Key(stadium_id) 
        references Stadium(id) on update cascade on delete set Null
);

Create Table H_Match(
    match_id int, 
    host_id int,
    Constraint PK_H_Match Primary Key (match_id),
    Constraint FK_H_Match1 Foreign Key(match_id) 
        references Match(id) on update cascade on delete cascade,
    Constraint FK_H_Match2 Foreign Key(host_id) 
        references Club(id) on update cascade
);

Create Table G_Match(
    match_id int, 
    guest_id int,
    Constraint PK_G_Match Primary Key (match_id),
    Constraint FK_G_Match1 Foreign Key(match_id) 
        references Match(id) on update cascade on delete cascade,
    Constraint FK_G_Match2 Foreign Key(guest_id)
        references Club(id) on update cascade
)

Create table Ticket(
        id int identity,
        status bit Default '1',
        match_id int,
        fan_id varchar(20),
        Constraint PK_Ticket Primary Key (id),
        Constraint FK_Ticket_Match Foreign Key(match_id)
            references Match(id) on update cascade on delete cascade,
        Constraint FK_Ticket_Fan Foreign Key(fan_id)
            references Fan(national_id) on update cascade on delete cascade

);

Go;

Create Procedure dropAllTables AS

    drop table H_Match;
    drop table G_Match;
    drop table SystemAdmin;
    drop table SportsAssociationManager;
    drop table Ticket;
    drop table Fan;
    drop table Match;
    drop table Host_Request;
    drop table Manager;
    drop table Stadium;
    drop table Representative;
    drop table Club;
    drop table SystemUser;    


Go;

Create Procedure dropAllProceduresFunctionsViews AS

    Drop Procedure createAllTables;
    Drop Procedure dropAllTables;
    Drop Procedure clearAllTables;
    Drop Procedure addAssociationManager;
    Drop Procedure addNewMatch;
    Drop Procedure deleteMatch;
    Drop Procedure deleteMatchesOnStadium;
    Drop Procedure addClub;
    Drop Procedure addTicket;
    Drop Procedure deleteClub;
    Drop Procedure addStadium;
    Drop Procedure deleteStadium;
    Drop Procedure blockFan;
    Drop Procedure unblockFan;
    Drop Procedure addRepresentative;
    Drop Procedure addHostRequest;
    Drop Procedure addStadiumManager;
    Drop Procedure acceptRequest;
    Drop Procedure rejectRequest;
    Drop Procedure addFan;
    Drop Procedure purchaseTicket;
    Drop Procedure updateMatchHost;
    Drop View allAssocManagers;
    Drop View allClubRepresentatives;
    Drop View allStadiumManagers;
    Drop View allFans;
    Drop View allMatches;
    Drop View allTickets;
    Drop View allCLubs;
    Drop View allStadiums;
    Drop View allRequests;
    Drop View clubsWithNoMatches;
    Drop View matchesPerTeam;
    Drop View clubsNeverMatched;
    Drop Function viewAvailableStadiumsOn;
    Drop Function allUnassignedMatches;
    Drop Function allPendingRequests;
    Drop Function upcomingMatchesOfClub;
    Drop Function availableMatchesToAttend;
    Drop Function clubsNeverPlayed;
    Drop Function matchWithHighestAttendance;
    Drop Function matchesRankedByAttendance;
    Drop Function requestsFromClub;

Go;

Create Procedure clearAllTables AS
    delete From H_Match;
    delete From G_Match;
    delete From SystemAdmin;
    delete From SportsAssociationManager;
    delete From Ticket;
    delete From Fan;
    delete From Match;
    delete From Host_Request;
    delete From Manager;
    delete From Stadium;
    delete From Representative;
    delete From Club;
    delete From SystemUser;
 
Go;

Create View allAssocManager AS
    Select S.username as 'Sport Association Manager Username',
           SU.password as 'Sport Association Manager Password',
           S.name as 'Sport Association Manager Name'
    From SportsAssociationManager S 
         Inner Join SystemUser SU on S.username = SU.username;

Go;

Create View allClubRepresentatives AS
    Select R.username as 'Representative Username',
        SU.password as 'Representative Password',
        R.name as 'Representative Name',
        C.name as 'Club Name'
    From Representative R
         Inner Join Club C on R.club_id = C.id
         Inner Join SystemUser SU on R.username = SU.username;

Go;

Create View allStadiumManagers AS
    Select M.username as 'Manager Username',
           SU.password as 'Manager Password',
           M.name as 'Manager Name',
           S.name as 'Stadium Name'
    From Manager M
         Inner Join Stadium S on M.stadium_id = S.id
         Inner Join SystemUser SU on M.username = SU.username;

Go;

Create View allFans AS
    Select F.username as 'Fan Username',
           SU.password as 'Fan Password',
           F.name as 'Fan Name',
           F.national_id as 'Fan National ID',
           F.birth_date as 'Fan Birth Date',
           F.status as 'Status'
    From Fan F
         Inner Join SystemUser SU on F.username = SU.username;

Go;

Create View allMatches AS
    Select C1.name as 'Host Club',
           C2.name as 'Guest Club',
           M.start_time as 'Start Time',
           M.end_time as 'End Time'
    From Match M Inner Join H_Match HM On M.id = HM.match_id 
        Inner Join Club C1 On HM.host_id = C1.id 
        Inner Join G_Match GM On M.id = GM.match_id
        Inner Join Club C2 On GM.guest_id = C2.id;

Go;

Create View allTickets AS
    Select C1.name as 'Host Club',
           C2.name as 'Guest Club',
           S.name as 'Hosting Stadium',
           M.start_time as 'Start Time'
    From Ticket T Inner Join Match M On T.match_id = M.id
        Inner Join H_Match HM On M.id = HM.match_id 
        Inner Join Club C1 On HM.host_id = C1.id 
        Inner Join G_Match GM On M.id = GM.match_id
        Inner Join Club C2 On GM.guest_id = C2.id
        Inner Join Stadium S On S.id=M.stadium_id;

Go;

Create View allClubs AS
    Select name as 'Club Name',location 'Club Location'
    From Club;
    
Go;

Create View allStadiums AS
    Select name as 'Stadium Name', 
           location 'Stadium Location', 
           capacity as 'Capacity', 
           status as 'Status'
    From Stadium;

Go;

Create View allRequests AS
    Select R.username as 'Representative Username',
        M.username as 'Manager Username',
        HR.status as 'Status'
    From Host_Request HR Inner Join Representative R On (HR.representative_id = R.id)
         Inner Join Manager M On (HR.manager_id = M.id);

Go;

Create Function SystemUserContains(@inpUserName varchar(20))
    Returns Bit 
    As
    Begin
        Declare @res Bit
        if Exists(Select S.username
                  From SystemUser S
                  where S.username = @inpUserName) 
            Set @res = '1'
        else 
            Set @res = '0'
        Return @res
    End

Go;

-- get type of the user
CREATE PROCEDURE typeOfUser
    @username varchar(20),
    @type INT OUTPUT
AS
    IF EXISTS (
        Select *
        FROM SystemAdmin
        where username=@username)
    BEGIN
        Set @type = 0
    END
    ELSE IF EXISTS(
        SELECT *
        FROM SportsAssociationManager
        WHERE username = @username)
    BEGIN
        SET @type = 1
    END
    ELSE IF EXISTS (
        SELECT *
        FROM Fan
        where username =@username)
    BEGIN
        SET @type = 2
    END
    ELSE IF EXISTS (
        SELECT *
        FROM Manager
        where username = @username)
    BEGIN
        SET @type = 3
    END
    ELSE IF EXISTS (
        SELECT *
        FROM Representative
        where username = @username)
    BEGIN
        SET @type = 4
    END
    ELSE
    BEGIN
        SET @type = 5
    END

Go;

Create Procedure checkCredentials
    @username varchar(20),
    @password varchar(20),
    @success bit output
AS
    IF Exists (
        Select * From SystemUser S
        Where S.username = @username and S.password = @password)
    BEGIN
        SET @success = '1'
    END
    ELSE
    BEGIN
        SET @success = '0'
    END

Go;

Create Procedure clubExists
    @clubName varchar(20),
    @success bit output
AS
    IF EXISTS (
        Select * From Club C
        Where C.name = @clubName)
    BEGIN
        SET @success = '1'
    END
    ELSE
    BEGIN
        SET @success = '0'
    END

Go;

Declare @success bit
exec stadiumExists 'Camp Nou', @success output
print @success

Go;

Create Procedure stadiumExists
    @stadiumName varchar(20),
    @success bit output
AS
    IF EXISTS (
        Select * From Stadium S
        Where S.name = @stadiumName)

        SET @success = '1'
    ELSE
        SET @success = '0'
    


Go;

Create View alreadyPlayedMatches AS
    Select C1.name as 'Host Name',
           C2.name as 'Guest Name',
           M.start_time as 'Start Time',
           M.end_time as 'End Time'
    From Club C1 
        Inner Join H_Match HM on HM.host_id = C1.id
        Inner Join Match M on HM.match_id = M.id
        Inner Join G_Match GM on GM.match_id = M.id
        Inner Join Club C2 on C2.id = GM.guest_id
    where M.start_time <= CURRENT_TIMESTAMP

Go;

Create Procedure getClubOfRepresentative 
@RepresentativeUname varchar(20),
@clubName varchar(20) output,
@clubLocation varchar(20) output
As
    Select @clubName = C.name, @clubLocation = C.location
    From Representative R 
         Inner Join Club C on R.club_id = C.id
    Where R.username = @RepresentativeUname

Go;

Create Procedure getStadiumOfManager
@ManagerUname varchar(20),
@stadiumName varchar(20) output,
@stadiumLocation varchar(20) output,
@stadiumCapacity int output
As
    Select @stadiumName = S.name, @stadiumLocation = S.location, @stadiumCapacity = S.capacity
    From Manager M
         Inner Join Stadium S on M.stadium_id = S.id
    Where M.username = @ManagerUname

Go;

Create View viewAllUpcomingMatches AS
    (
    Select C1.name as 'Host Name',
           C2.name as 'Guest Name',
           M.start_time as 'Start Time',
           M.end_time as 'End Time'
    From Club C1 
        Inner Join H_Match HM on HM.host_id = C1.id
        Inner Join Match M on HM.match_id = M.id
        Inner Join G_Match GM on GM.match_id = M.id
        Inner Join Club C2 on C2.id = GM.guest_id
    where M.start_time > CURRENT_TIMESTAMP
    )

Go;

Create Function viewRequestsOfManager (@managerUsername varchar(20))
Returns Table
AS
    Return(
        Select R.name as 'Representative Name',
            C1.name as 'Host Name',
            C2.name as 'Guest Name',
            M.start_time as 'Start Time',
            M.end_time as 'End Time',
            HR.status as 'Request Status'
        From Host_Request HR Inner Join Representative R  
            on HR.representative_id = R.id 
            Inner Join Manager Ma on Ma.id = HR.manager_id
            Inner Join Match M on HR.match_id = M.id
            Inner Join H_Match HM on HM.match_id = M.id
            Inner Join Club C1 on HM.host_id = C1.id
            Inner Join G_Match GM on GM.match_id = M.id
            Inner Join Club C2 on GM.guest_id = C2.id
        Where  Ma.username = @managerUsername)

Go;

Create Function availableMatchesForFan(@start_time DateTime)
    Returns Table
    As
        Return (
            Select C1.name as 'Host Club Name',
                   C2.name as 'Guest Club Name',
                   S.name as 'Stadium Name',
                   S.location as 'Stadium Location'
            From Match M
                Inner Join Ticket T on M.id = T.match_id
                Inner Join H_Match HM on M.id = HM.match_id
                Inner Join Club C1 on HM.host_id = C1.id
                Inner Join G_Match GM on M.id = GM.match_id
                Inner Join Club C2 on GM.guest_id = C2.id
                Inner Join Stadium S on S.id = M.stadium_id
            where T.status = '1' AND
                M.start_time >= @start_time
            Group By C1.name, C2.name, M.start_time, S.name, S.location
            Order By M.start_time
            offset 0 rows
        )

Go ;

Create Procedure getMatchTime
@hostName varchar(20),
@guestName varchar(20),
@startTime datetime output
As
    Select @startTime = M.start_time
    From Match M
        Inner Join H_Match HM on M.id = HM.match_id
        Inner Join G_Match GM on M.id = GM.match_id
        Inner Join Club C1 on C1.id = HM.host_id
        Inner Join Club C2 on C2.id = GM.guest_id
    Where C1.name = @hostName and C2.name = @guestName

Go;

Create Procedure getFanNationalID
@fanUsername varchar(20),
@fanNationalID varchar(20) output
As
    Select @fanNationalID = F.national_id
    From Fan F
    where F.username = @fanUsername

Go;

-- Requirement no. 1 done
Create Procedure addAssociationManager 
    @assocManagerName varchar(20),
    @assocManagerUsername varchar(20),
    @assocManagerPassword varchar(20),
    @success Bit Output
AS
    if dbo.SystemUserContains(@assocManagerUsername) = '0'
        Begin
            Insert Into SystemUser
            Values(@assocManagerUsername, @assocManagerPassword)

            Insert Into SportsAssociationManager
            Values(@assocManagerUsername, @assocManagerName)

            Set @success = '1'
        End
    else
        Set @success = '0'

Go;

-- Requirement no. 2 done
Create Procedure addNewMatch
    @hostClubName varchar(20),
    @guestClubName varchar(20),
    @matchTime datetime,
    @endTime datetime,
    @success bit output
AS
    IF Exists (
        Select *
        From Match M
            Inner Join H_Match H on M.id = H.match_id
            Inner Join G_Match G on M.id = G.match_id
            Inner Join Club C1 on C1.id = H.host_id
            Inner Join Club C2 on C2.id = G.guest_id
        Where (C1.name = @hostClubName And M.start_time = @matchTime) OR (C2.name = @hostClubName And M.start_time = @matchTime)
            OR (C1.name = @guestClubName And M.start_time = @matchTime) OR (C2.name = @guestClubName And M.start_time = @matchTime))
    BEGIN
        SET @success = '0'
    END
    ELSE
    BEGIN
        Insert Into Match (start_time, end_time)
        Values (@matchTime, @endTime)

        Declare @reqMatchID int
        Select @reqMatchID = max(M.ID)
        From Match M
        Where M.start_time = @matchTime

        Declare @hostID int
        Select @hostID = C.id
        From Club C
        Where C.name = @hostClubName

        Declare @guestID int
        Select @guestID = C.id
        From Club C
        Where C.name = @guestClubName

        Insert Into H_Match
        Values (@reqMatchID, @hostID)

        Insert Into G_Match
        Values (@reqMatchID, @guestID)
        
        SET @success = '1'
    END

Go;

-- Requirement no. 3 done
Create View clubsWithNoMatches AS
    Select C.name As 'Club Name'
    From Club C
    Where Not Exists (Select C1.id 
                      From Club C1, H_Match HM
                      Where C1.id = HM.host_id and C1.id = C.id) and
          Not Exists (Select C2.id 
                        From Club C2, G_Match GM
                        Where C2.id = GM.guest_id and C2.id = C.id);

Go;

-- Requirement no. 4 done
Create Procedure deleteMatch
    @hostClubName varchar(20),
    @guestClubName varchar(20)
AS            
    Declare @reqMatchID int
    Select @reqMatchID = min(H.match_id)
    From H_Match H , G_Match G
    Where H.match_id = G.match_id
        and (Select H1.name
             From Club H1
             Where H1.id = H.host_id) = @hostClubName
        and (Select G1.name
             From Club G1
             Where G1.id = G.guest_id) = @guestClubName

    delete from Host_Request Where match_id = @reqMatchID
    delete from Match Where id = @reqMatchID

Go;

--Requirement no. 5 done
Create Procedure deleteMatchesOnStadium
    @stadiumName varchar(20)
AS
    Declare @reqStadiumID int
    Select @reqStadiumID = S.id
    From Stadium S
    Where S.name = @stadiumName

    Delete From Host_Request
    Where match_id in (
        Select M.id
        From Match M
        Where M.stadium_id = @reqStadiumID
        and M.start_time > CURRENT_TIMESTAMP)

    Delete From Match
    Where stadium_id = @reqStadiumID
        and start_time > CURRENT_TIMESTAMP
  
Go;

-- Requirement no. 6 done
Create Procedure addClub
    @clubName varchar(20),
    @clubLocation varchar(20)
AS
    Insert Into Club
    Values (@clubName, @clubLocation)

Go;

-- Requirement no. 7 done
Create Procedure addTicket
    @hostClub varchar(20),
    @guestClub varchar(20),
    @startingTime datetime
AS
    Declare @reqMatchID int
    Select @reqMatchID = M.id
    From H_Match HM
         Inner Join G_Match GM on HM.match_id = GM.match_id
         Inner Join Club C1 on C1.id = HM.host_id
         Inner Join Club C2 on C2.id = GM.guest_id
         Inner Join Match M on M.id = GM.match_id
    where C1.name = @hostClub And C2.name = @guestClub
          And M.start_time = @startingTime

    Insert Into Ticket(match_id)
    Values (@reqMatchID)
             
Go;

-- Requirement no. 8
Create Procedure deleteClub
    @clubName varchar(20)
AS
    Declare @club_id int 
    Select @club_id = C.id
    From Club C
    where C.name = @clubName

    Delete From Match
    Where id in (
        Select M.id
        From Match M 
             Inner Join H_Match HM on M.id = HM.match_id
             Inner Join G_Match GM on M.id = GM.match_id
             Inner Join Club CH on CH.id = HM.host_id
             Inner Join Club CG on CG.id = GM.guest_id
        Where (CH.name = @clubName or CG.name = @clubName)
    )
    
    -- Deleting Representative and Host_Req
    Declare @representative_id int 
    Select @representative_id = R.id
    From Representative R
    where R.club_id = @club_id
    
    Delete From Host_Request
    where representative_id = @representative_id

    Delete From Club 
    Where id = @club_id

Go;

--Requirement no. 9 done
Create procedure addStadium
    @stadiumName varchar(20),
    @stadiumLocation varchar(20),
    @stadiumCapacity int
AS
    insert into Stadium (location,capacity,name) values 
                          (@stadiumLocation,@stadiumCapacity,@stadiumName);
  
Go;

--Requirement no. 10
Create procedure deleteStadium
    @stadiumName varchar(20)
AS
    Declare @stadiumId int;
    select @stadiumId=S.id 
    from Stadium S
    where S.name=@stadiumName;
    
    Delete From Ticket
    Where match_id in (
        Select M.id
        From Match M
        Where M.stadium_id = @stadiumId
    )

    --Deleting StadiumManager
    Declare @manager_id int 
    Select @manager_id = Ma.id
    From Manager Ma
    where Ma.stadium_id = @stadiumId
    
    Delete from Stadium
    where name=@stadiumName;
    
Go;

--Requirement no. 11  done
Create procedure blockFan
    @fan_national_id varchar(20)
AS
    update Fan 
    set status= '0' 
    where national_id= @fan_national_id

Go;

--Requirement no. 12  done
Create procedure unblockFan
    @fan_national_id varchar(20)
AS
    update Fan 
    set status= '1' 
    where national_id= @fan_national_id

Go;

--Requirement no. 13 done
Create procedure addRepresentative
    @representativeName varchar(20),
    @representativeClubname varchar(20),
    @representativeUsername varchar(20),
    @representativePassword varchar(20),
    @success Bit Output
AS
    if dbo.SystemUserContains(@representativeUsername) = '0' 
    Begin
          Insert into SystemUser 
          values (@representativeUsername, @representativePassword);

          Declare @clubId int;
          select @clubId = min(C.id)
          from Club C
          where C.name =  @representativeClubname ;

          Insert into Representative
          Values ( @representativeUsername, @representativeName , @clubId) ;

          Set @success = '1' ;

     END
     else
        Set @success = '0' ;

Go;

--Requirement no. 14 done
Create Function viewAvailableStadiumsOn(@date DateTime)
    Returns table
    As
    return
    (
    select S.name as 'Stadium Name',
            S.location as 'Stadium Location',
            S.capacity as 'Stadium Capacity'
    from stadium S
    where S.status = '1' AND
            not exists ( select * 
                       from Match M
                       where M.stadium_id is not null and 
                             M.stadium_id=S.id AND (DateAdd(minute,-90,M.start_time) < @date and @date < M.end_time) )
    )
           
Go;

Create Procedure hostRequestExists
    @clubName varchar(20), 
    @stadiumName varchar(20),
    @datetime DateTime,
    @success bit output
AS
    If Exists (
        Select HR.id
        From Host_Request HR
            Inner Join Representative R on R.id = HR.representative_id
            Inner Join Club C on C.id = R.club_id
            Inner Join Manager Ma on Ma.id = HR.manager_id
            Inner Join Stadium S on S.id = Ma.stadium_id
            Inner Join Match M on M.id = HR.match_id
        Where C.name = @clubName AND S.name = @stadiumName AND M.start_time = @datetime)
        
        SET @success = '1'

    Else
        
        SET @success = '0'

Go;

Create Procedure matchExists
    @clubRepUsername varchar(20),
    @startTime DateTime,
    @success bit output
AS
    IF EXISTS (
        (Select M.id 
        From Club C
            Inner Join Representative R on R.club_id = C.id
            Inner Join H_Match HM on HM.host_id = C.id
            Inner Join Match M on M.id = HM.match_id
        Where R.username = @clubRepUsername AND M.start_time = @startTime)
        Union
        (Select M.id 
        From Club C
            Inner Join Representative R on R.club_id = C.id
            Inner Join G_Match GM on GM.guest_id = C.id
            Inner Join Match M on M.id = GM.match_id
        Where R.username = @clubRepUsername AND M.start_time = @startTime))
        
        SET @success = '1'
    
    ELSE

        SET @success = '0'


Go;

--Requirement no. 15 done
Create procedure addHostRequest 
    @clubName varchar(20), 
    @stadiumName varchar(20),
    @datetime DateTime
AS
    
    Declare @match_id int;
    Declare @manager_id int;
    Declare @representative_id int;
    
    Select @manager_id=Ma.id
    From Stadium S, Manager MA
    where S.id = Ma.stadium_id 
        And S.name = @stadiumName

    Select @representative_id=R.id 
    From Representative R, Club C
    where R.club_id = C.id And
        C.name = @clubName

    Select @match_id = M.id
    From Match M
        Inner Join H_Match HM on M.id = HM.match_id 
        Inner Join Club C on HM.host_id = C.id
    where   M.start_time = @datetime And
            C.name = @clubName
    
    If @match_id Is Not Null
    Begin
        Insert into Host_Request (match_id,manager_id,representative_id)
        values (@match_id, @manager_id, @representative_id);
    End


Go;

--Requirement no. 16 done
Create function allUnassignedMatches(@hostClub varchar(20))
 Returns table
    As
       return(
       select C_guest.name as 'Name of Competing Club', 
       M.start_time as 'Start Time of Match'
       from Club C_host, Club C_guest, Match M , H_Match HM, G_Match GM
       where C_host.name=@hostClub AND C_host.id=HM.host_id
            AND HM.match_id=M.id AND M.stadium_id is null
            AND GM.match_id=M.id AND GM.guest_id=C_guest.id 
              );

Go;

-- Requirement no. 17 done
Create Procedure addStadiumManager
    @managerName varchar(20),
    @stadiumName varchar(20),
    @managerUsername varchar(20),
    @managerPassword varchar(20),
    @success Bit Output
As
    if dbo.SystemUserContains(@managerUsername) = '0'
       Begin
        Insert Into SystemUser
        Values(@managerUsername, @managerPassword);

        Declare @reqStadiumID int  ;
        Select @reqStadiumID = min(S.id)
        From Stadium S
        where S.name = @stadiumName ;

        Insert Into Manager 
        Values(@managerUsername, @managerName, @reqStadiumID) ;

        Set @success = '1' ;
       End
     else
        Set @success = '0' ;
Go;

--Requirement no. 18 done
Create Function allPendingRequests (@inpStadiumManager varchar(20))
    Returns Table 
    As
       Return (
           Select R.name as 'Name of Club Representative sending the Request',
                C.name as 'Name of Club competing with Sender',
                M.start_time as 'Start Time of Match to be hosted'
            From Host_Request HR Inner Join Representative R  
                on HR.representative_id = R.id 
                Inner Join Manager Ma on Ma.id = HR.manager_id
                Inner Join Match M on HR.match_id = M.id 
                Inner Join G_Match GM on GM.match_id = M.id
                Inner Join Club C on GM.guest_id = C.id
            Where  Ma.username = @inpStadiumManager 
                and HR.status = 'Unhandled'
        )

Go;

--helper function that returns host_req_ID_to_be_updated for requirements 19, 20
Create Function get_host_req_ID_to_be_updated
    (@inp_stadium_manager_username varchar(20),
    @inp_hosting_club_name varchar(20),
    @inp_competing_club_name varchar(20),
    @inp_start_time_of_match DateTime )

    Returns int
    As
    Begin
        Declare @host_req_ID_to_be_updated int 
        Select @host_req_ID_to_be_updated = min(HR.id)
        From Host_Request HR Inner Join Manager Ma 
            on HR.manager_id = Ma.id 
            Inner Join Representative R on R.id = HR.representative_id
            Inner Join Match M on M.id = HR.match_id
            Inner Join G_Match GM on GM.match_id = M.id
            Inner Join Club CHost on CHost.id = R.club_id
            Inner Join Club CGuest on CGuest.id = GM.guest_id
        where @inp_stadium_manager_username = Ma.username 
            AND @inp_hosting_club_name = CHost.name
            AND @inp_competing_club_name = CGuest.name
            AND @inp_start_time_of_match = M.start_time 
        Return @host_req_ID_to_be_updated;
    End

Go;

-- Requiremrnt no. 19 done
Create Procedure acceptRequest 
    @inp_stadium_manager_username varchar(20),
    @inp_hosting_club_name varchar(20),
    @inp_competing_club_name varchar(20),
    @inp_start_time_of_match DateTime
AS
        Declare @host_req_ID_to_be_updated int 
        Set @host_req_ID_to_be_updated =
            dbo.get_host_req_ID_to_be_updated(
                @inp_stadium_manager_username,
                @inp_hosting_club_name,
                @inp_competing_club_name,
                @inp_start_time_of_match
            )

        Declare @stadiumID int
        Select @stadiumID = SM.stadium_id
        From Manager SM
        Where SM.username = @inp_stadium_manager_username

        Declare @matchID int
        Select @matchID = HR.match_id
        From Host_Request HR
        Where HR.id = @host_req_ID_to_be_updated

        Declare @stadiumStatus bit
        Select @stadiumStatus = S.status
        From Stadium S
        Where S.id = @stadiumID

        if @stadiumStatus = 1
        Begin
            Update Host_Request 
            Set status = 'Accepted' 
            Where id = @host_req_ID_to_be_updated

            Update Match
            Set stadium_id = @stadiumID
            Where id = @matchID

            Declare @capacity int
            Select @capacity = S.capacity
            From Stadium S
            Where S.id = @stadiumID

            DECLARE @Counter INT 
            SET @Counter=1
            WHILE ( @Counter <= @capacity)
            BEGIN
                Exec addTicket @inp_hosting_club_name, @inp_competing_club_name, @inp_start_time_of_match
                SET @Counter  = @Counter  + 1
            END
        End

Go;

-- Requiremrnt no. 20 done
Create Procedure rejectRequest 
    @inp_stadium_manager_username varchar(20),
    @inp_hosting_club_name varchar(20),
    @inp_competing_club_name varchar(20),
    @inp_start_time_of_match DateTime
    As
        Declare @host_req_ID_to_be_updated int 
        Set @host_req_ID_to_be_updated =
            dbo.get_host_req_ID_to_be_updated(
                @inp_stadium_manager_username,
                @inp_hosting_club_name,
                @inp_competing_club_name,
                @inp_start_time_of_match
            )

        Update Host_Request 
        Set status = 'Rejected' 
        Where id = @host_req_ID_to_be_updated

Go;

--Requirement no. 21 done
Create Procedure addFan
    @inp_name varchar(20),
    @inp_username varchar(20),
    @inp_password varchar(20),
    @inp_national_id varchar(20),
    @inp_birth_date DateTime,
    @inp_address varchar(20),
    @inp_phone_number bigint,
    @success Bit Output

As
       if dbo.SystemUserContains(@inp_username) = '0'
       Begin
        Insert Into SystemUser
        Values(@inp_username, @inp_password);

        Insert Into Fan 
        Values(@inp_username, @inp_national_id, @inp_name,
            @inp_address, @inp_phone_number, '1', @inp_birth_date) ;
        Set @success = '1' ;
       End
       else
        Set @success = '0' ;

 
Go;

-- Requirement no. 22 done
Create Function upcomingMatchesOfClub (@inp_club_name varchar(20))
    Returns Table

    As
        Return (
            (
                Select C1.name as 'Given Club Name',
                       C2.name as 'The Competing Club Name',
                       M.start_time as 'Start Time of Match',
                       M.end_time as 'End Time of Match',
                       S.name as 'Stadium Name'
                From Club C1 
                     Inner Join H_Match HM on HM.host_id = C1.id
                     Inner Join Match M on HM.match_id = M.id
                     Inner Join G_Match GM on GM.match_id = M.id
                     Inner Join Club C2 on C2.id = GM.guest_id
                     Left Outer Join Stadium S on S.id = M.stadium_id
                where C1.name = @inp_club_name 
                    AND M.start_time > CURRENT_TIMESTAMP

            )
             Union 
            (
                Select C1.name as 'Given Club Name',
                       C2.name as 'The Competing Club Name',
                       M.start_time as 'Start Time of Match',
                       M.end_time as 'End Time of Match',
                       S.name as 'Stadium Name'
                From Club C1 
                     Inner Join G_Match GM on GM.guest_id = C1.id
                     Inner Join Match M on GM.match_id = M.id
                     Inner Join H_Match HM on HM.match_id = M.id
                     Inner Join Club C2 on C2.id = HM.host_id
                     Left Outer Join Stadium S on S.id = M.stadium_id
                where C1.name = @inp_club_name 
                    AND M.start_time > CURRENT_TIMESTAMP
            )

        )

Go;

--requirement no. 23 done
Create Function availableMatchesToAttend(@start_time DateTime)
    Returns Table
    As
        Return (
            Select C1.name as 'Host Club Name',
                   C2.name as 'Guest Club Name',
                   M.start_time as 'Match Start Time',
                   S.name as 'Stadium Name',
                   S.location as 'Stadium Location'
            From Match M
                Inner Join Ticket T on M.id = T.match_id
                Inner Join H_Match HM on M.id = HM.match_id
                Inner Join Club C1 on HM.host_id = C1.id
                Inner Join G_Match GM on M.id = GM.match_id
                Inner Join Club C2 on GM.guest_id = C2.id
                Inner Join Stadium S on S.id = M.stadium_id
            where T.status = '1' AND
                M.start_time >= @start_time
            Group By C1.name, C2.name, M.start_time, S.name, S.location
            Order By M.start_time
            offset 0 rows
        )

Go;

-- Requirement no. 24 done
Create Procedure purchaseTicket
    @inp_national_id varchar(20),
    @inp_host_club varchar(20),
    @inp_competing_club varchar(20),
    @inp_match_date DateTime,
    @success Bit output
AS
    Declare @fanStatus bit
    Select @fanStatus = F.status
    From Fan F
    Where F.national_id = @inp_national_id

    if @fanStatus = '1'
        Begin
            Declare @ticket_id int
            Select @ticket_id = min(T.id)
            From Ticket T
                    Inner Join Match M on T.match_id = M.id
                    Inner Join (H_Match HM
                            Inner Join Club C1 on HM.host_id=C1.id) on M.id=HM.match_id
                    Inner Join (G_Match GM
                            Inner Join Club C2 on GM.guest_id=C2.id) on M.id=GM.match_id
                    Where T.match_id = M.id
                            and C1.name = @inp_host_club 
                            and C2.name = @inp_competing_club
                            and M.start_time = @inp_match_date
                            and T.status = '1'

            Update Ticket
            Set status = '0',
                fan_id = @inp_national_id
            Where id = @ticket_id
            Set @success = '1' 
        End
    else
        Set @success = '0'

Go;

-- Requirement no. 25 done
create procedure updateMatchHost
    @Host_club_name varchar(20), 
    @Competing_club_name varchar(20),
    @Date_of_match datetime

As
    Declare @host_id int,
            @guest_id int,
            @match_id int

    select @host_id = c1.id , @guest_id = c2.id , @match_id=M.id
    From Club c1 , Club c2 , H_Match H ,G_match G , Match M
    where ((@Host_club_name = c1.name) AND (@Competing_club_name = c2.name)) AND (c1.id = H.host_id) AND
           (c2.id = G.guest_id) AND(H.match_id = G.match_id) AND 
           (M.id = H.match_id) AND (M.start_time = @Date_of_match);

  delete from H_Match where H_Match.host_id = @host_id AND H_Match.match_id = @match_id;
  insert into H_Match values (@match_id,@guest_id);
  delete from G_Match where G_Match.guest_id = @guest_id AND G_Match.match_id = @match_id;
  insert into G_Match values (@match_id,@host_id);
  
Go;

--helper function for requirement 26
Create Function getTempTable()
Returns Table
As

    Return (

        (
            select c.name as 'club_name',count(m.id) as 'number_of_matches_played'
            From Match m inner join H_Match h on(m.id = h.match_id)
                            inner join Club c on (c.id = h.host_id)
            where  (m.start_time<CURRENT_TIMESTAMP)
            group by c.name 
        )
        UNION All
        (
            select c.name as 'club_name',count(m.id) as 'number_of_matches_played'
            From Match m inner join G_Match g on(m.id = g.match_id)
                            inner join Club c on (c.id = g.guest_id)
            where  (m.start_time<CURRENT_TIMESTAMP)
            group by c.name
        )

   )

Go;

--Requirement no. 26 done
Create View matchesPerTeam
AS
    Select T1.club_name As 'Club Name', sum(T1.number_of_matches_played) As 'Number Of Matches Played'
    From dbo.getTempTable() T1
    Group By T1.club_name
    

Go;                    

-- Requirement no. 27 done
Create view clubsNeverMatched 
 AS
         (
         select c1.name as 'First Club', c2.name as 'Second Club'
         from Club c1, Club c2
         where c1.id<c2.id
         )
        Except
        (
            
            (    select c1.name as 'first club', c2.name as 'second club'
                From Match m inner join H_match h on(m.id = h.match_id)
                             inner join G_Match g on(m.id = g.match_id)
                             inner join Club c1 on (c1.id = h.host_id)
                             inner join Club c2 on (c2.id = g.guest_id)
                where (h.match_id = g.match_id)
            )
            Union 
            (
               select c2.name as 'first club' , c1.name as 'second club'
               From Match m inner join H_match h on(m.id = h.match_id)
                             inner join G_Match g on(m.id = g.match_id)
                             inner join Club c1 on (c1.id = h.host_id)
                             inner join Club c2 on (c2.id = g.guest_id)
                where (h.match_id = g.match_id)
            )
        ) 

Go ;

-- Requirement no. 28 done
Create function clubsNeverPlayed (@clubName varchar(20))
returns table
 AS
 RETURN(
         (
         select c1.name as 'Club Name'
         from Club c1
         )
        Except
        (
            
            (   select c1.name as 'clubName'
                From Match m inner join H_match h on(m.id = h.match_id)
                             inner join G_Match g on(m.id = g.match_id)
                             inner join Club c1 on (c1.id = h.host_id)
                             inner join Club c2 on (c2.id = g.guest_id)
                where c2.name=@clubName
            )
            Union 
            (
               select c2.name as 'clubName'
                From Match m inner join H_match h on(m.id = h.match_id)
                             inner join G_Match g on(m.id = g.match_id)
                             inner join Club c1 on (c1.id = h.host_id)
                             inner join Club c2 on (c2.id = g.guest_id)
                where c1.name=@clubName
            )
            Union
            (
                select c.name
                from Club c
                where c.name = @clubName
            )
        ) 
)

Go;

-- Requirement no. 29 done
create function matchWithHighestAttendance ()
 returns table
 AS
    return (
            select c1.name as 'Host Club',c2.name as 'Guest Club'        
            From Ticket t inner join Match m on(t.match_id = m.id)
                     inner join H_Match h on(m.id = h.match_id)
                     inner join G_Match g on(m.id = g.match_id)
                     inner join Club c1 on(c1.id = h.host_id)
                     inner join club c2 on(c2.id = g.guest_id)
            where t.status='0'
            group by c1.name , c2.name
            having count(*) >= ALL  (Select count(*)  
                                    From Ticket T , Match m1
                                    where m1.id = T.match_id AND T.status='0'
                                    group by m1.id)                              
            )

Go;

-- Requirement no. 30 done
Create function matchesRankedByAttendance()
returns table 
    As
    return 
    (
          select c1.name as 'Host Club',
                c2.name as 'Guest Club',
                count(t.id) as 'Total Number Of Tickets'
          From Match m inner join H_match h on(m.id = h.match_id)
                 inner join G_Match g on(m.id = g.match_id)
                 inner join Club c1 on (c1.id = h.host_id)
                 inner join Club c2 on (c2.id = g.guest_id)
                 inner join Ticket t on (m.id = t.match_id)
          where t.status='0'
          group by c1.name, c2.name
          order by count(t.id) desc
          offset 0 rows
     )

Go;

-- Requirement no. 31 done
create function requestsFromClub
    (@stadium_name varchar(20) , @club_name varchar(20))
    returns table
    return (select c1.name as 'Host Club', c2.name as 'Guest Club'
          From Match m inner join H_match h on(m.id = h.match_id)
                 inner join G_Match g on(m.id = g.match_id)
                 inner join Club c1 on (c1.id = h.host_id)
                 inner join Club c2 on (c2.id = g.guest_id)
                 inner join Representative R on (R.club_id=c1.id)
                 inner join Host_Request HR on (HR.representative_id=R.id)
                 inner join Manager ma on (ma.id = HR.manager_id)
                 inner join Stadium S on (ma.stadium_id = S.id)
          where c1.name=@club_name AND S.name=@stadium_name AND m.id=HR.match_id
          )