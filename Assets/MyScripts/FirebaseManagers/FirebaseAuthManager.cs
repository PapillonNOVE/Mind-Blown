using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System.Threading.Tasks;
using Constants;
using System;

public class FirebaseAuthManager : MonoBehaviour
{
    private void OnEnable()
    {
        Subscribe();   
    }

    private void OnDisable()
    {
        GeneralControls.ControlQuit(Unsubscribe);
    }

    private void Subscribe()
    {
        EventManager.Instance.SignUpWithEmailPassword += SignUpWithEmailPassword;
        EventManager.Instance.SignInWithEmailPassword += SignInWithEmailPassword;
        EventManager.Instance.ResetPasswordWithEmail += ResetPasswordWithEmail;
        EventManager.Instance.SignOut += SignOut;
        EventManager.Instance.DeleteUser += DeleteUser;
    }

	private void Unsubscribe()
	{
		EventManager.Instance.SignUpWithEmailPassword -= SignUpWithEmailPassword;
		EventManager.Instance.SignInWithEmailPassword -= SignInWithEmailPassword;
		EventManager.Instance.ResetPasswordWithEmail -= ResetPasswordWithEmail;
		EventManager.Instance.SignOut -= SignOut;
		EventManager.Instance.DeleteUser -= DeleteUser;
	}

	// private void AuthStateChanged(object sender, EventArgs eventArgs)
	// {
	//     if (FirebaseManager.auth.CurrentUser != FirebaseManager.user)
	//     {
	//         bool signedIn = FirebaseManager.user != FirebaseManager.auth.CurrentUser && FirebaseManager.auth.CurrentUser != null;
	//         if (!signedIn && FirebaseManager.user != null)
	//         {
	//             Debug.Log("Signed out " + FirebaseManager.user.UserId);
	//             ActionManager.Instance.ShowSignInPanel();

	//             FirebaseManager.UserDatabaseReference = FirebaseManager.UserNullDatabaseReference;
	//         }
	//         FirebaseManager.user = FirebaseManager.auth.CurrentUser;
	//         if (signedIn)
	//         {
	//             Debug.Log("Signed in " + FirebaseManager.user.UserId);
	//             FirebaseManager.displayName = FirebaseManager.user.DisplayName ?? "";
	//             FirebaseManager.emailAddress = FirebaseManager.user.Email ?? "";
	//             FirebaseManager.SetUserDatabaseReference();
	//	ActionManager.Instance.GetCurrentUserProfile();
	//}
	//     }
	//     else
	//     {
	//         Debug.Log("Kullanıcı yok galiba");
	//     }
	// }

	#region Sign Up

	private IEnumerator SignUpWithEmailPassword(SignUpStruct signUpStruct, Action onSuccessCallback, Action onFailCallback)
    {
        Task task = FirebaseManager.auth.CreateUserWithEmailAndPasswordAsync(signUpStruct.Email, signUpStruct.Password);

        yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

        if (task.IsCanceled)
        {
            Debug.LogWarning(AuthenticationsDebugs.SignUp + DebugPaths.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            onFailCallback();
            Debug.LogError(AuthenticationsDebugs.SignUp + DebugPaths.IsFaulted);
        }
        else if (task.IsCompleted)
        {
            //FirebaseManager.SetUserDatabaseReference();

            EventManager.Instance.CreatUserProfile(signUpStruct.Username, signUpStruct.Language);

            onSuccessCallback();
            Debug.Log(AuthenticationsDebugs.SignUp + DebugPaths.IsCompleted);
        }
    }

    #endregion

    #region Sign In

    private IEnumerator SignInWithEmailPassword(string email, string password, Action onSuccessCallback, Action onFailCallback)
    {
        Task task = FirebaseManager.auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

        if (task.IsCanceled)
        {
            Debug.LogWarning(AuthenticationsDebugs.SignIn + DebugPaths.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            onFailCallback();
            Debug.LogError(AuthenticationsDebugs.SignIn + DebugPaths.IsFaulted);
        }
        else if (task.IsCompleted)
        {
            //FirebaseManager.SetUserDatabaseReference();

            onSuccessCallback();
            Debug.Log(AuthenticationsDebugs.SignIn + DebugPaths.IsCompleted);
            // ActionManager.Instance.UpdateUserData(UserPaths.General,UserPaths.SignInStatus,true);
        }
    }

    #endregion

    #region Reset Password

    private IEnumerator ResetPasswordWithEmail(string email, Action onSuccessCallback, Action onFailCallback)
    {
        Task task = FirebaseManager.auth.SendPasswordResetEmailAsync(email);

        yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

        if (task.IsCanceled)
        {
            Debug.LogWarning(AuthenticationsDebugs.ResetPassword + DebugPaths.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            onFailCallback();
            Debug.LogError(AuthenticationsDebugs.ResetPassword + DebugPaths.IsFaulted);
        }

        onSuccessCallback();
        Debug.Log(AuthenticationsDebugs.ResetPassword + DebugPaths.IsCompleted);
    }

    #endregion

    #region Sign Out

    private void SignOut(Action onSuccessCallback, Action onFailCallback)
    {
        Debug.Log(AuthenticationsDebugs.SignOut + DebugPaths.IsCompleted);
        EventManager.Instance.UpdateUserData(UserPaths.PrimaryPaths.General, UserPaths.GeneralPaths.SignInStatus, false);
        //FirebaseManager.AuthStateChanged(this, null);
        UIManager.Instance.ShowSignInPanel();

        FirebaseManager.auth.SignOut();

        onSuccessCallback();
    }

    #endregion

    #region Delete User

    private void DeleteUser()
    {
        FirebaseManager.auth.CurrentUser.DeleteAsync();
        //FirebaseUser user = auth.CurrentUser;
        /*auth.CurrentUser.DeleteAsync().ContinueWith(task =>
           {
               Debug.Log("Kullanıcı silme işlemi deneniyor");

               if (task.IsCanceled)
               {
                   Debug.Log("Kullanıcı silme işlemi iptal edildi.");
               }
               else if (task.IsFaulted)
               {
                   Debug.Log("Kullanıcı silme işlemi başarısız oldu.");
               }
               else if (task.IsCompleted)
               {
                   Debug.Log("Kullanıcı silme işlemi başarıyla tamamlandı.");
               }
           });*/

    }

    #endregion
}
