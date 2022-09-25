module.exports = function (api) {
  api.cache(true);
  return {
    plugins: ["tailwindcss-react-native/babel"],
    presets: ['babel-preset-expo'],
  };
};
