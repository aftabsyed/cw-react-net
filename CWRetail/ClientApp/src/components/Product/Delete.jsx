import React, { Component } from "react";
import axios from "axios";

export class Delete extends Component {
  constructor(props) {
    super(props);

    this.onConfirmation = this.onConfirmation.bind(this);
    this.onCancel = this.onCancel.bind(this);

    this.state = {
      name: "",
      price: 0.0,
      type: "",
      isActive: null,
    };
  }

  componentDidMount() {
    const { id } = this.props.match.params;

    axios.get("/api/Product/" + id).then((product) => {
      const response = product.data;

      this.setState({
        name: response.name,
        price: response.price,
        type: response.type,
        isActive: response.active,
      });
    });
  }

  onCancel(e) {
    const { history } = this.props;
    history.push("/products");
  }

  onConfirmation(e) {
    const { id } = this.props.match.params;

    e.preventDefault();    

    axios.delete("api/Product/" + id).then((result) => {
        this.props.history.push("/products");
      });
  }

  render() {
    return (
      <div style={{ marginTop: 10 }}>
        <h2>Delete Product confirmation</h2>
        <div className="card">
          <div className="card-body">
            <h4 className="card-title"> {this.state.name} </h4>            
            <button onClick={this.onConfirmation} className="btn btn-danger">
              Confirm
            </button>
            <button onClick={this.onCancel} className="btn btn-default">
              Cancel
            </button>
          </div>
        </div>
      </div>
    );
  }
}

//export default Delete;
