import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'
import { Products } from './components/Product/Products';
import { Create } from './components/Product/Create';
import { Update } from './components/Product/Update';
import { Delete } from './components/Product/Delete';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/products' component={Products} />
        <Route path='/create' component={Create} />
        <Route path='/update/:id' component={Update} />
        <Route path='/delete/:id' component={Delete} />
      </Layout>
    );
  }
}
