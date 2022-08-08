import { registerRootComponent } from 'expo';

// Assuming `App.js` is the root component.
import App from './App';

// Tells Expo to install the root React component in your `index.html` document body.
// This runs `AppRegistry.registerComponent()` -> `ReactDOM.render()`
// behind a web-only flag.
registerRootComponent(App);