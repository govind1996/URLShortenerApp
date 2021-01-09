import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureStore from './store/configureStore'
import AppRoute from './routers/AppRouter'
import registerServiceWorker from './registerServiceWorker';
import { checkAuthState } from './actions/auth';
// import './styles/style.scss';

const rootElement = document.getElementById('root');

const store = configureStore();

store.subscribe(()=>{
  console.log(store.getState());
});
// setTimeout(()=>{
//   store.dispatch(addLink({
//     id:1,
//     shortenUrl:'shortify.com/1',
//     orignalUrl:'google.com',
//   }))
// },3000);
// store.dispatch(addLink({
//   id:1,
//   shortenUrl:'shortify.com/1',
//   orignalUrl:'google.com',
// }))
store.dispatch(checkAuthState());
const jsx = (
  <Provider store = {store}>
    <AppRoute />
  </Provider>

)

ReactDOM.render(jsx, rootElement);

registerServiceWorker();

