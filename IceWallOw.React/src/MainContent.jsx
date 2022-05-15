import React, { Component } from "react";
export default class MainContent extends Component {

    state = { pageTitle: "Customers", customersCount: 5 };

    render() {
        return (
            <div>
                <h4 className="border-bottom m-1 p-1"> {this.state.pageTitle}
                    <span style={{ backgroundColor: "#6c757d" }} className="badge badge-secondary" >{this.state.customersCount}</span>
                    <button className="btn btn-info" onClick={this.onRefreshClick}>Refresh</button>
                </h4>
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