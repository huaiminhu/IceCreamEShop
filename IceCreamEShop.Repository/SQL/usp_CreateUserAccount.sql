CREATE PROCEDURE dbo.usp_CreateUserAccount
    @Email NVARCHAR(256),
    @EnPassword NVARCHAR(256),
    @UserName NVARCHAR(100),
    @PhoneNumber NVARCHAR(20) = NULL,
    @UserRole INT,
    @IsActive BIT = 1,
    @NewUserAccountId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.UserAccount (
            Email, 
            EnPassword, 
            UserName, 
            PhoneNumber, 
            UserRole, 
            IsActive, 
            CreatedAt, 
            UpdatedAt
        )
        VALUES (
            @Email, 
            @EnPassword, 
            @UserName, 
            @PhoneNumber, 
            @UserRole, 
            @IsActive, 
            GETDATE(), 
            NULL
        );

        -- 取得自動遞增的 ID 並賦值給輸出參數
        SET @NewUserAccountId = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END;
GO
