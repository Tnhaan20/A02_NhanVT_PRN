using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A02_BOs;

namespace A02_Repos
{
    public interface IAccountRepository
    {
        public SystemAccount GetAccount(String email, String password);

        public List<SystemAccount> getAllAccounts();
        public SystemAccount GetAccountById(short accountId);
        public SystemAccount GetAccountByEmail(String email);
        public void AddAccount(SystemAccount account);
        public void UpdateAccount(short accId, SystemAccount account);
        public void DeleteAccount(short accountId);
        public List<object> GetSelectAccountList();

    }
}
