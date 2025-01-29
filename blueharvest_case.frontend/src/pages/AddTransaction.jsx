import { useState } from "react";
import { addTransaction } from "../api/api";

function AddTransaction() {
    const [accountId, setAccountId] = useState("");
    const [amount, setAmount] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        await addTransaction(Number(accountId), Number(amount));
        alert("Transaction added successfully!");
    };

    return (
        <div>
            <h1>Add Transaction</h1>
            <form onSubmit={handleSubmit}>
                <input type="text" placeholder="Account ID" value={accountId} onChange={(e) => setAccountId(e.target.value)} />
                <input type="text" placeholder="Amount" value={amount} onChange={(e) => setAmount(e.target.value)} />
                <button className="button">Add</button>
            </form>
        </div>
    );
}

export default AddTransaction;
