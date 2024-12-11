// public string ChangeAge(int id, int age)
// {
//     if (age < 18 || age > 150)
//     {
//         return "Age must be between 18 and 150";
//     }

//     AccountModel account = GetById(id);
//     if (account != null)
//     {
//         account.Age = age;
//         UpdateList(account);
//         return "Age changed successfully";
//     }
//     else
//     {
//         return "Account not found";
//     }
// }



// public string ChangeEmail(int id, string newEmail)
// {
//     if (string.IsNullOrWhiteSpace(newEmail))
//     {
//         return "Email is required";
//     }

//     AccountModel account = GetById(id);
//     if (account != null)
//     {
//         if (!CheckEmailInJson(newEmail))
//         {
//             account.EmailAddress = newEmail;
//             UpdateList(account);
//             return "Email changed successfully";
//         }
//         else
//         {
//             return "Email already exists";
//         }
//     }
//     else
//     {
//         return "Account not found";
//     }
// }