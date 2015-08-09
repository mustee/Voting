namespace Voting.Data.DbScripts
{
    public partial class DbScripts
    {
		public class User
        {
            public static string Insert =
                @"INSERT INTO [User](FirstName, LastName, MiddleName, Password, Salt, Email, DateJoined, 
                Country, MobileNumber, MobileNumberCode, MobileNumberConfirmed, ConfirmationToken, Confirmed, DateConfirmed, LastLogin,
                AuthType, AuthID, Deleted, ForgotPasswordToken, ForgotPasswordTimeStamp, Role) OUTPUT INSERTED.Id
				VALUES (@FirstName, @LastName, @MiddleName, @Password, @Salt, @Email, @DateJoined, 
                @Country, @MobileNumber, @MobileNumberCode, @MobileNumberConfirmed, @ConfirmationToken, @Confirmed, @DateConfirmed, 
                @LastLogin, @AuthType, @AuthID, @Deleted, @ForgotPasswordToken, @ForgotPasswordTimeStamp, @Role)";

            public static string Update =
                @"UPDATE [User] SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, 
                Password = @Password, Salt = @Salt, Email = @Email, DateJoined = @DateJoined, 
                Country = @Country, MobileNumber =@MobileNumber, MobileNumberCode = @MobileNumberCode,
                MobileNumberConfirmed = @MobileNumberConfirmed, ConfirmationToken = @ConfirmationToken, 
                Confirmed = @Confirmed, DateConfirmed = @DateConfirmed, LastLogin = @LastLogin, 
                AuthType = @AuthType, AuthID = @AuthID, Deleted = @Deleted, ForgotPasswordToken = @ForgotPasswordToken,
                ForgotPasswordTimeStamp = @ForgotPasswordTimeStamp, Role = @Role
                WHERE Id = @Id";

            public static string Select = @"SELECT * FROM [User] WHERE Id = @Id";

			public static string FindByEmail = @"SELECT * FROM [User] WHERE Email = @Email";

            public static string FindByConfirmationToken = @"SELECT * FROM [User] WHERE ConfirmationToken = @ConfirmationToken";

            public static string FindByForgotPasswordToken = @"SELECT * FROM [User] WHERE ForgotPasswordToken = @ForgotPasswordToken";

            public static string FindAll = @"SELECT * FROM [User] WHERE Deleted = 0";

            public static string Delete = @"UPDATE [User] SET Deleted = 1 WHERE Id = @Id";

            public static readonly string FindByLogin =
                @"SELECT u.* FROM [User] u INNER JOIN ExternalLogin l ON l.UserId = u.Id
                where l.LoginProvider = @LoginProvider and l.ProviderKey = @ProviderKey";

            public static readonly string FindByAuthTypeAndAuthId =
                @"SELECT * FROM [User] WHERE AuthType = @AuthType AND AuthId = @AuthId";
        }
    }
}
