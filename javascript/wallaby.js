module.exports = function (wallaby) {
  return {
    files: [
      'src/**/*.js',
      '!src/**/*test.js'
    ],

    tests: [
      'src/2018/**/*test.js'
    ],

    env: {
      type: 'node'
    },

    compilers: {
      '**/*.js': wallaby.compilers.babel()
    },

    testFramework: 'jest'
  };
};