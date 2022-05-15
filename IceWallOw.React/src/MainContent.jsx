import React, { Component } from "react";
export default class MainContent extends Component {

    state = { appTitle: "Customers" };

    render() {
        return (
            <div>
                <h4 className="border-bottom m-1 p-1"> {this.state.appTitle}</h4>
            </div>
        )
    }
}