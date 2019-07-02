import { i18n } from 'element-react/next';
import locale from 'element-react/src/locale/lang/en';
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App/App';
import * as serviceWorker from './serviceWorker';
import { compose, applyMiddleware, createStore } from 'redux';
import { Provider } from 'react-redux';
import rootReducer from './Store/rootReducer';
import thunk from 'redux-thunk';
import '@fortawesome/fontawesome-free/css/all.min.css';

i18n.use(locale);

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
const myStore = createStore(rootReducer, composeEnhancers(applyMiddleware(thunk)));

ReactDOM.render(
	<Provider store={myStore}>
		<App />
	</Provider>,
	document.getElementById('root')
);
serviceWorker.unregister();
