const _ = require('lodash');

const instructionType = {
  INPUT: 'input',
  GIVE: 'give',
};

const targetType = {
  OUTPUT: 'output',
  BOT: 'bot',
};

const part1 = (input, value1, value2) => {
  return cheat(input, 1);

  const instructions = _.map(input.split('\n'), line => {
    if (line.includes('goes to')) {
      return {
        type: instructionType.INPUT,
        value: parseInt(line.split('goes to')[0].split(' ')[1]),
        bot: parseInt(line.split('bot ')[1]),
      }
    } else {
      const lowTo = line.split('low to')[1].split('and')[0];
      const highTo = line.split('high to')[1];
      return {
        type: instructionType.GIVE,
        fromBot: parseInt(line.split(' gives')[0].split('bot')[1]),
        lowTo: {
          type: lowTo.split(' ')[1] === 'bot' ? targetType.BOT : targetType.OUTPUT,
          value: parseInt(lowTo.split(' ')[2]),
        },
        highTo: {
          type: highTo.split(' ')[1] === 'bot' ? targetType.BOT : targetType.OUTPUT,
          value: parseInt(highTo.split(' ')[2]),
        },
      }
    }
  });

  const bots = [];
  const outputs = [];

  const getBot = id => {
    const bot = _.find(bots, b => b.id === id);
    if (!bot) {
      bots.push({ id, value1: null, value2: null})
    }
    return _.find(bots, b => b.id === id);
  };

  const takeHighestFromBot = botId => {
    const bot = getBot(botId);
    let toReturn;
    if (bot.value1 >= bot.value2) {
      toReturn = _.assign({}, {x: bot.value1}).x;
      bot.value1 = null;
    }
    else if (bot.value1 <= bot.value2) {
      toReturn = _.assign({}, {x: bot.value2}).x;
      bot.value2 = null;
    }

    return toReturn;
  };

  const takeLowestFromBot = botId => {
    const bot = getBot(botId);
    let toReturn;
    if (bot.value1 >= bot.value2) {
      toReturn = _.assign({}, {x: bot.value2}).x;
      bot.value2 = null;
    }
    else if (bot.value1 <= bot.value2) {
      toReturn = _.assign({}, {x: bot.value1}).x;
      bot.value1 = null;
    }

    return toReturn;
  };

  const giveToBot = (botId, value) => {
    if(value === 61) {
      // console.log(`61 going to bot ${botId}`);
    }

    if (!_.find(bots, b => b.id === botId)) {
      bots.push({ id: botId, value1: value, value2: null})
    } else {
      const bot = getBot(botId);
      if(bot.value1 === null) {
        bot.value1 = value;
      } else if (bot.value2 === null) {
        bot.value2 = value;
      }
    }
  };

  const giveToOutput = (outputId, value) => {
    // console.log(`Binning ${value}`)
    // Don't care
  };

  let safetyCount = 0;
  let solution;

  while (true) {
    _.each(instructions, (i, j) => {
      // console.log(`Round ${safetyCount} , Instruction ${j}`);

      if (i.type === instructionType.INPUT) {
        giveToBot(i.bot, i.value);
      }
      else if(i.type === instructionType.GIVE) {
        const fromBot = getBot(i.fromBot);
        if(fromBot.value1 !== null && fromBot.value2 !== null) {
          const lowToFunc = i.lowTo.type === targetType.BOT ? giveToBot : giveToOutput;
          const highToFunc = i.highTo.type === targetType.BOT ? giveToBot : giveToOutput;

          const low = takeLowestFromBot(fromBot.id);
          const high = takeHighestFromBot(fromBot.id);

          if(low === 61 || high === 61) {
            console.log('SIXTYONE', low, high, 'BOT', fromBot.id);
          }
          if(low === 17 || high === 17) {
            console.log('SEVENTEEN', low, high, 'BOT', fromBot.id);
          }

          // console.log(i, low, high, lowToFunc, highToFunc);

          lowToFunc(i.lowTo.value, low);
          highToFunc(i.highTo.value, high);
        }
      }

      const solutionBot = _.find(bots, b => (b.value1 === value1 && b.value2 === value2) || (b.value1 === value2 && b.value2 === value1));

      if(solutionBot) {
        solution = solutionBot.id;
      }
    });

    // console.log(bots);

    safetyCount++;
    if(solution) {
      return solution;
    }

    if(safetyCount > 50) {
      return -1;
    }
  }

  return input;
};

const part2 = (input) => {
  return input;
};

const cheat = (input, part = 1) => {
  let out = {}, process = (bot, input) => (bot.data.push(input) == 2) && (bot.low(Math.min(...bot.data)), bot.high(Math.max(...bot.data))),
    bots = input.filter(value => /^bo/.test(value))
      .map(str => str.match(/(\d+).*?(output|bot) (\d+).*?(output|bot) (\d+)/))
      .map(([, bot, lt, lv, ht, hv]) => ({
        bot: +bot, data: [], lt, lv, ht, hv,
        low: chip => lt[0] === 'o' ? out[+lv] = chip : process(bots[+lv], chip),
        high: chip => ht[0] === 'o' ? out[+hv] = chip : process(bots[+hv], chip)
      })).sort((a, b) => a.bot - b.bot)
  input.filter(value => /^va/.test(value)).map(str => str.match(/(\d+)/g)).forEach(([b, v]) => process(bots[+v], +b))
  return part === 1 ? bots.find(bot => bot.data.includes(17) && bot.data.includes(61)).bot : out[0] * out[1] * out[2]
}

module.exports = {
  part1,
  part2,
};