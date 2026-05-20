-- 1. Tabela za mapiranje između Jira i ASEE tiketa
CREATE TABLE dbo.TicketMappings (
    Id INT PRIMARY KEY IDENTITY(1,1),
    JiraTicketKey NVARCHAR(50) NOT NULL UNIQUE,
    JiraTicketId NVARCHAR(100) NOT NULL,
    AseeTicketId NVARCHAR(100) NOT NULL UNIQUE,
    SyncStatus NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE', -- ACTIVE, CLOSED, PAUSED
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE(),
    LastSyncedAt DATETIME2 NULL,
    INDEX IX_JiraTicketKey (JiraTicketKey),
    INDEX IX_AseeTicketId (AseeTicketId),
    INDEX IX_SyncStatus (SyncStatus)
);

-- 2. Tabela za čuvanje logova sinhronizacije
CREATE TABLE dbo.SyncLogs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TicketMappingId INT NOT NULL,
    SyncDirection NVARCHAR(20) NOT NULL, -- 'JIRA_TO_ASEE', 'ASEE_TO_JIRA'
    ActionType NVARCHAR(50) NOT NULL, -- 'CREATE', 'UPDATE', 'CLOSE', etc.
    SourceSystem NVARCHAR(20) NOT NULL, -- 'JIRA', 'ASEE'
    TargetSystem NVARCHAR(20) NOT NULL,
    Details NVARCHAR(MAX) NULL, -- JSON sa detaljima promene
    Status NVARCHAR(20) NOT NULL DEFAULT 'SUCCESS', -- SUCCESS, FAILED, PENDING
    ErrorMessage NVARCHAR(MAX) NULL,
    ExecutedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (TicketMappingId) REFERENCES dbo.TicketMappings(Id) ON DELETE CASCADE,
    INDEX IX_TicketMappingId (TicketMappingId),
    INDEX IX_SyncDirection (SyncDirection),
    INDEX IX_ExecutedAt (ExecutedAt)
);

-- 3. Tabela za čuvanje polja tiketa (za praćenje šta se promenilo)
CREATE TABLE dbo.TicketFieldSnapshots (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TicketMappingId INT NOT NULL,
    SnapshotSystem NVARCHAR(20) NOT NULL, -- 'JIRA', 'ASEE'
    FieldName NVARCHAR(100) NOT NULL,
    OldValue NVARCHAR(MAX) NULL,
    NewValue NVARCHAR(MAX) NULL,
    ChangedAt DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (TicketMappingId) REFERENCES dbo.TicketMappings(Id) ON DELETE CASCADE,
    INDEX IX_TicketMappingId (TicketMappingId),
    INDEX IX_ChangedAt (ChangedAt)
);

-- 4. Tabela za čuvanje greške i ponovnih pokušaja
CREATE TABLE dbo.SyncRetries (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TicketMappingId INT NOT NULL,
    SyncDirection NVARCHAR(20) NOT NULL,
    RetryCount INT DEFAULT 0,
    MaxRetries INT DEFAULT 3,
    LastRetryAt DATETIME2 NULL,
    NextRetryAt DATETIME2 NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'PENDING', -- PENDING, RETRYING, COMPLETED, FAILED
    ErrorMessage NVARCHAR(MAX) NULL,
    FOREIGN KEY (TicketMappingId) REFERENCES dbo.TicketMappings(Id) ON DELETE CASCADE,
    INDEX IX_TicketMappingId (TicketMappingId),
    INDEX IX_NextRetryAt (NextRetryAt)
);

-- 5. Tabela za čuvanje konfiguracije mapiranja polja
CREATE TABLE dbo.FieldMappingConfig (
    Id INT PRIMARY KEY IDENTITY(1,1),
    JiraFieldName NVARCHAR(100) NOT NULL,
    AseeFieldName NVARCHAR(100) NOT NULL,
    FieldType NVARCHAR(50) NOT NULL, -- 'TEXT', 'SELECT', 'DATE', etc.
    IsMappingRequired BIT DEFAULT 0,
    TransformationRule NVARCHAR(MAX) NULL, -- JSON sa pravilima transformacije ako je potrebna
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UNIQUE(JiraFieldName, AseeFieldName)
);
