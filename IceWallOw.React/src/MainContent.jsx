import React, { Component } from "react";
export default class MainContent extends Component {

    state = {
        pageTitle: "Customers",
        customersCount: 5,
        customers: [

            { id: 1, name: "Scott", phone: "123-456", address: { city: "myszogrud" } },
            { id: 2, name: "Jones", phone: "234-456", address: { city: "orlean" } },
            { id: 3, name: "James", phone: "123-426", address: { city: "kolno" } },
            { id: 4, name: "Alex", phone: "122-123", address: { city: "lomza" } },
            { id: 5, name: "John", phone: "555-666", address: { city: "bydgoszcz" } },
        ],
    };

    render() {
        return (
            <div>
                <h4 className="border-bottom m-1 p-1"> {this.state.pageTitle}
                    <span style={{ backgroundColor: "#6c757d" }} className="badge badge-secondary" >{this.state.customersCount}</span>
                    <button className="btn btn-info" onClick={this.onRefreshClick}>Refresh</button>
                </h4>
                <table className="table">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Customer Name</th>
                            <th>Phone</th>
                            <th>City</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.customers.map((cust) => {
                                return (
                                    <tr key={cust.id}>
                                        <td>{cust.id}</td>
                                        <td>{cust.name}</td>
                                        <td>{cust.phone}</td>
                                        <td>{cust.address.city}</td>
                                    </tr>
                                );
                            })
                        }

                    </tbody>

                </table>
            </div>
        )
    }


    onRefreshClick = () => {
        console.log("refresh click")
        this.setState({
            customersCount: 7
        });
    }
}