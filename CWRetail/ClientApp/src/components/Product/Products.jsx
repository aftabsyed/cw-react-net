import React, { Component } from "react";
import axios from "axios";
import "font-awesome/css/font-awesome.min.css";

export class Products extends Component {
  constructor(props) {
    super(props);

    this.state = {
      products: [],
      loading: true,
    };

    this.onProductAdd = this.onProductAdd.bind(this);
    this.onProductUpdate = this.onProductUpdate.bind(this);
    this.onProductDelete = this.onProductDelete.bind(this);
  }

  componentDidMount() {
    this.populateProductsData();
  }

  populateProductsData() {
    axios.get("api/Product").then((result) => {
      const response = result.data;
      this.setState({ products: response, loading: false });
    });
  }

  onProductUpdate(id) {
    const { history } = this.props;
    history.push("/update/" + id);
  }

  onProductAdd(id) {
    const { history } = this.props;
    history.push("/create");
  }

  onProductDelete(id) {
    const { history } = this.props;
    history.push("/delete/" + id);
  }

  renderProductsTable(products) {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Price</th>
            <th>Type</th>
            <th>Active</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.id}>
              <td>{product.name}</td>
              <td>{product.price}</td>
              <td>{product.type}</td>
              <td>{product.active ? "Yes" : "No"}</td>
              <td>
                <div className="form-group">
                  <button
                    className="btn btn-primary btn-sm rounded-0 list-inline-item"
                    onClick={() => this.onProductAdd(product.id)}
                  >
                    <i class="fa fa-table"></i>
                  </button>
                  <button
                    className="btn btn-success btn-sm rounded-0 list-inline-item"
                    onClick={() => this.onProductUpdate(product.id)}
                  >
                    <i class="fa fa-edit"></i>
                  </button>
                  <button
                    className="btn btn-danger btn-sm rounded-0 "
                    onClick={() => this.onProductDelete(product.id)}
                  >
                    <i class="fa fa-trash"></i>
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let content = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderProductsTable(this.state.products)
    );

    return (
      <div>
        <h1>All products</h1>
        <p>Here you can see all products</p>
        {content}
      </div>
    );
  }
}
