
namespace ConstantKeeper
{
	public static class DebugPaths
	{
		public static readonly string IsCanceled = " iptal edildi!";
		public static readonly string IsFaulted = " başarısız oldu!";
		public static readonly string IsCompleted = " başarıyla tamamlandı!";
	}

	public static class AuthenticationsDebugs
	{
		public static readonly string SignIn = "Oturum açma işlemi";
		public static readonly string SignUp = "Kayıt olma işlemi";
		public static readonly string SignOut = "Oturum kapatma işlemi";
		public static readonly string ResetPassword = "Şifre sıfırlama bağlantısı gönderme";
		public static readonly string DeleteUser = "Kullanıcı silme işlemi";

		public static class SignUpPaths
		{
			public static readonly string SignUpFailed = "Kayıt işlemi başarısız";
			public static readonly string SignUpFailedDetail = "";
	
			public static readonly string SignUpSuccessful = "Kayıt işlemi başarılı!";
			public static readonly string SignUpSuccessfulDetail = "Ana menüye yönlendiriliyorsunuz...";
		}


		public static class SignInPaths
		{
			public static readonly string SignInSuccessful = "Giriş işlemi başarılı!";
			public static readonly string SignInSuccessfulDetails = "Ana menüye yönlendiriliyorsunuz...";

			public static readonly string SignInFailed = "Giriş işlemi başarısız!";
			public static readonly string SignInFailedDetails = "Sabit bir internet bağlantınız olduğundan emin olun. E-posta ve şifrenizin doğru olduğundan emin olun veya sıfırlayın. Eğer bir hesabınız yoksa, KAYIT olabilirsiniz...";
		}

		public static class SignOutPaths 
		{
			public static readonly string SignOutSuccessful = "Çıkış işlemi başarılı!";
			public static readonly string SignOutSuccessfulDetails = "Oturum kapatıldı giriş ekranına yönlendiriyorsunuz...";
			
			public static readonly string SignOutFailed = "Çıkış işlemi başarısız!";
			public static readonly string SignOutFailedDetails = "Bilinmeyen hata! Lütfen daha sonra tekrar deneyiniz...";
		}

		public static class ResetPasswordPaths 
		{
			public static readonly string ResetPasswordSuccessful = "Şifre sıfırlama işlemi başarılı!";
			public static readonly string ResetPasswordSuccessfulDetails = "Şifre sıfırlama bağlantısı gönderildi. Lütfen e-posta hesabınızı kontrol ediniz, şifre sıfırlama bağlantısı \"Spam\" klasöründe olabilir...";
			
			public static readonly string ResetPasswordFailed = "Şifre sıfırlama işlemi başarısız!";
			public static readonly string ResetPasswordFailedDetails = "E-postanızı doğru girdiğinizden emin olun. Eğer bir hesabınız yoksa kayıt olabilirsiniz...";
		}
	}

	public static class GetDataTaskDebugs
	{
		public static readonly string GetData = "Veri çekme işlemi";
	}

	public static class UserTaskDebugs
	{
		public static readonly string GetCurrentUserProfile = "Kullanıcı verileri çekme işlemi";
		public static readonly string UpdateCurrentUserProfile = "Kullanıcı verileri güncelleme işlemi";
		public static readonly string DeleteCurrentUserProfile = "Kullanıcı verileri silme işlemi";
	}

	public static class UserPaths
	{
		// Main Paths
		public static readonly string Users = "Users";
		public static readonly string UserID = "UserID";

		public static class PrimaryPaths
		{
			// Primary Paths
			public static readonly string General = "General";
			public static readonly string Progression = "Progression";
			public static readonly string Consumable = "Consumable";
		}

		public static class GeneralPaths
		{
			// String General Paths
			public static readonly string Username = "Username";
			public static readonly string Country = "Country";
			public static readonly string Language = "Language";
			public static readonly string SignUpDate = "SignUpDate";
			public static readonly string LastSeen = "LastSeen";

			// Bool General Paths
			public static readonly string SignInStatus = "SignInStatus";
			public static readonly string Intermateable = "Intermateable";
		}

		public static class ProgressionPaths
		{
			// Int Progression Paths
			public static readonly string Level = "Level";
			public static readonly string Cup = "Cup";
			public static readonly string Rank = "Rank";
			public static readonly string HighScore = "HighScore";
			public static readonly string TotalPlayTime = "TotalPlayTime";
			public static readonly string TotalMatches = "TotalMatches";
			public static readonly string CompletedMatches = "CompletedMatches";
			public static readonly string CorrectAnswers = "CorrectAnswers";
			public static readonly string WrongAnswers = "WrongAnswers";
			public static readonly string AbandonedMatches = "AbandonedMatches";
			public static readonly string Wins = "Wins";
			public static readonly string Losses = "Losses";
			public static readonly string WinningStreak = "WinningStreak";
		}

		public static class ConsumablePaths
		{
			// Int Consumable Paths
			public static readonly string Gem = "Gem";
			public static readonly string Papcoin = "Papcoin";
			public static readonly string Energy = "Energy";
		}
	}

	public static class GameSettingsPaths
	{
		// Main Paths
		public static readonly string GameSettings = "GameSettings";
		
		// Secondary Paths
		public static readonly string Localization = "Localization";
		public static readonly string SoundSettings = "SoundSettings";

		/*// Localization
		public static readonly string AbandonedMatches = "AbandonedMatches";
		public static readonly string Cancel = "Cancel";
		public static readonly string CompletedMatches = "CompletedMatches";
		public static readonly string Cup = "Cup";
		public static readonly string Gem = "Gem";
		public static readonly string Energy = "Energy";
		public static readonly string Estimate = "Estimate";
		public static readonly string Papcoin = "Papcoin";
		public static readonly string LastSeen = "LastSeen";
		public static readonly string Level = "Level";
		public static readonly string Loading = "Loading";
		public static readonly string Lose = "Lose";
		public static readonly string Losses = "Losses";
		public static readonly string Music = "Music";
		public static readonly string Ok = "Ok";
		public static readonly string Online = "Online";
		public static readonly string Password = "Password";
		public static readonly string Play = "Play";
		public static readonly string QuickGame = "QuickGame";
		public static readonly string Rank = "Rank";
		public static readonly string RateUs = "RateUs";
		public static readonly string Rival = "Rival";
		public static readonly string Score = "Score";
		public static readonly string SecretNumber = "SecretNumber";
		public static readonly string Settings = "Settings";
		public static readonly string SignIn = "SignIn";
		public static readonly string SignOut = "SignOut";
		public static readonly string SignUp = "SignUp";
		public static readonly string Store = "Store";
		public static readonly string TotalTimePlayed = "TotalTimePlayed";
		public static readonly string TotalMatches = "TotalMatches";
		public static readonly string UserProfile = "UserProfile";
		public static readonly string Username = "Username";
		public static readonly string Vibration = "Vibration" ;
		public static readonly string Volume = "Volume";
		public static readonly string Win = "Win";
		public static readonly string WinningStreak = "WinningStreak";
		public static readonly string Wins = "Wins";
		public static readonly string You = "You";
		*/


		
	}

	public static class RoomPaths 
	{
		// Main Paths
		public static readonly string Rooms = "Rooms";

		// Secondary Paths
		public static readonly string RoomID = "RoomID";
		public static readonly string General = "General";
		public static readonly string Progression = "Progression";
		
		// General Paths
		public static readonly string P1_ID = "P1_ID";
		public static readonly string P1_Username = "P1_Username";
		public static readonly string P2_ID = "P2_ID";
		public static readonly string P2_Username = "P2_Username";
		public static readonly string ScoreLimit = "ScoreLimit";
		public static readonly string AnswerTimeLimit = "AnswerTimeLimit";
		public static readonly string PlayerLimit = "PlayerLimit";
		public static readonly string SecretNumber = "SecretNumber";
		public static readonly string SecretNumberMaxValue = "SecretNumberMaxValue";
		public static readonly string Penetrability = "Penetrability";

		// Progression Paths
		public static readonly string LastEstimation = "LastEstimation";
		public static readonly string WhoseTurn = "WhoseTurn";
	}

	public static class QuestionPaths
	{
		public static readonly string Questions = "Questions";

		public static class PrimaryPaths
		{
			public static readonly string PublishedQuestions = "PublishedQuestions";
			public static readonly string PendingQuestions = "PendingQuestions";
		}


		public static class QuesitonPath
		{

			public static readonly string Question = "Question";
			public static readonly string CorrectOption = "CorrectOption";
			public static readonly string WrongOption1 = "WrongOption1";
			public static readonly string WrongOption2 = "WrongOption2";
			public static readonly string WrongOption3 = "WrongOption3";
			public static readonly string Category = "Category";

			public static class PrimaryCategories
			{
				public static readonly string Mixed = "Mixed";
				public static readonly string Science = "Science";
				public static readonly string Art = "Art";
			}

			public static class ArtCategories
			{
				public static readonly string MartialArt = "MartialArt";
				public static readonly string Movie = "Movie";
				public static readonly string Music = "Music";
				public static readonly string VideoGame = "VideoGame";
				public static readonly string Book = "Book";
				public static readonly string Anime = "Anime";
			}

			public static class ScienceCategories
			{
				public static readonly string Math = "Math";
				public static readonly string Language = "Language";
				public static readonly string Technology = "Technology";
				public static readonly string History = "History";
			}
		}
	}
}
