# MvcCRUD
留言板

# 架構

# Model     MvcCRUD/Mvc20201107/Models/Guesbooks.cs
Model裡面的定義都是依照DB裡面的table欄位去定義且那些資料需要做欄位驗證
# Control   MvcCRUD/Mvc20201107/Controllers/GuestbooksController.cs 
# Service   MvcCRUD/Mvc20201107/Services/GuestbooksDBService.cs + MvcCRUD/Mvc20201107/Services/ForPaging.cs 
# ViewModel MvcCRUD/Mvc20201107/ViewModels/GuestbooksViewModel.cs 
# View      MvcCRUD/Mvc20201107/Views/Guestbooks/

#  MvcCRUD/Mvc20201107/Web.config   <connectionStrings>
    <add name="ASP.NET.MVC" connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=Mvc_basic;Integrated Security=True" providerName="System.Data.SqlClient"/>
  </connectionStrings>
下面是定義連線到資料庫的字串

