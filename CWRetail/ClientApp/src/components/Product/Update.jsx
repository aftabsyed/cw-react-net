import React, { Component } from "react";
import axios from "axios";
//import ToggleButton from 'react-bootstrap/ToggleButton'

export class Update extends Component {
  constructor(props) {
    super(props);

    this.onChangeName = this.onChangeName.bind(this);
    this.onChangePrice = this.onChangePrice.bind(this);
    this.onChangeType = this.onChangeType.bind(this);
    this.onChangeActive = this.onChangeActive.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
    this.onCancel = this.onCancel.bind(this);

    this.state = {
      name: "",
      price: 0.0,
      type: "",
      isActive: false,
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

  onChangeName(e) {
    this.setState({
      name: e.target.value,
    });
  }

  onChangePrice(e) {
    this.setState({
      price: e.target.value,
    });
  }

  onChangeType(e) {
    this.setState({
      type: e.target.value,
    });
  }
  onChangeActive(e) {
    this.setState({
      isActive: e.target.checked,
    });
  }

  onCancel(e) {
    const { history } = this.props;
    history.push("/products");
  }

  onSubmit(e) {
    e.preventDefault();

    const { id } = this.props.match.params;

    let productObject = {
      id: id,
      name: this.state.name,
      price: this.state.price,
      type: this.state.type,
      active: this.state.isActive,
    };

    axios.put("api/Product/" + id, productObject).then((result) => {
      this.props.history.push("/products");
    });
  }

  render() {
    return (
      <div className="product-form">
        <h3>Update Product</h3>
        <form onSubmit={this.onSubmit}>
          <div className="form-group">
            <label>Name: </label>
            <input
              type="text"
              className="form-control"
              value={this.state.name}
              onChange={this.onChangeName}
            />
          </div>
          <div className="form-group">
            <label>Price: </label>
            <input
              type="number"
              className="form-control"
              value={this.state.price}
              onChange={this.onChangePrice}
            />
          </div>
          <div className="row">
            <div className="col col-md-6 col-sm-6 col-xs-12">
              <div className="form-group">
                <label>Type</label>
                <select
                  value={this.state.type}
                  className="form-control"
                  onChange={this.onChangeType}
                >
                    <option value="">Select</option>
                  <option value="Books">Books</option>
                  <option value="Electronics">Electronics</option>
                  <option value="Food">Food</option>
                  <option value="Furniture">Furniture</option>
                  <option value="Toys">Toys</option>
                </select>
              </div>
            </div>
            <div className="col col-md-6 col-sm-6 col-xs-12">
              <div className="form-group">
                <label> Active </label>
                <input
                  name="isActive"
                  type="checkbox"
                  className="form-control"
                  checked={this.state.isActive}
                  onChange={this.onChangeActive}
                />
              </div>
            </div>
          </div>

          <div className="form-group">
            <button type="submit" className="btn btn-primary">
              Update Product
            </button>
            <button onClick={this.onCancel} className="btn btn-default">
              Cancel
            </button>
          </div>
        </form>
      </div>
    );
  }
}
