import { useEffect, useState } from "react";
import { getAccounts } from "../api/api";

function Home() {
    const [accounts, setAccounts] = useState([]);
    const customerId = 1; // Statik ID, ileride giriþ yapan kullanýcýnýn ID'si olabilir

    useEffect(() => {
        getAccounts(customerId).then((response) => {
            setAccounts(response.data.accounts);
        });
    }, []);

    return (
        <div>
            <h1>Accounts</h1>
            {accounts.length > 0 ? (
                <ul>
                    {accounts.map((account) => (
                        <li key={account.accountId} className="list-item">
                            <p>Account ID: {account.accountId}</p>
                            <p>Balance: ${account.balance}</p>
                        </li>
                    ))}
                </ul>
            ) : (
                <p>No accounts found.</p>
            )}
        </div>
    );
}

export default Home;
