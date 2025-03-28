﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A02_BOs;
using A02_DAOs;

namespace A02_Repos
{
    public class AccountRepository : IAccountRepository
    {
        public List<SystemAccount> getAllAccounts() => AccountDAO.Instance.getAllAcounts();
        public SystemAccount GetAccount(string email, string password) => AccountDAO.Instance.GetAccount(email, password);

        public SystemAccount GetAccountByEmail(string email) => AccountDAO.Instance.GetAccount(email);

        public SystemAccount GetAccountById(short accountId) => AccountDAO.Instance.getAccByID(accountId);

        public void AddAccount(SystemAccount account) => AccountDAO.Instance.AddAccount(account);

        public void DeleteAccount(short accountId) => AccountDAO.Instance.RemoveAccount(accountId);


        public void UpdateAccount(short accountId, SystemAccount account) => AccountDAO.Instance.UpdateAccount(accountId, account);

        public List<object> GetSelectAccountList() => AccountDAO.Instance.GetSelectAccountList();
    }
}
