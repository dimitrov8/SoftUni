function solve(input) {
  const nCharacters = Number(input.shift());
  const characters = {};

  // Call functions
  parseCharacters();
  processCommands();
  print();

  function parseCharacters() {
    for (let i = 0; i < nCharacters; i++) {
      const [characterName, health, bullets] = input.shift().split(' ');

      characters[characterName] = {
        health: Number(health),
        bullets: Number(bullets),
      };
    }
  }

  function processCommands() {
    let commandInput = input.shift();
    while (commandInput !== 'Ride Off Into Sunset') {
      const [command, characterName, ...rest] = commandInput.split(' - ');

      if (command === 'FireShot') {
        const target = rest[0];

        fireShot(characterName, target);
      } else if (command === 'TakeHit') {
        const damage = Number(rest[0]);
        const attacker = rest[1];

        takeHit(characterName, damage, attacker);
      } else if (command === 'Reload') {
        reload(characterName);
      } else if (command === 'PatchUp') {
        const amount = Number(rest[0]);

        patchUp(characterName, amount);
      }

      commandInput = input.shift();
    }
  }

  function fireShot(characterName, target) {
    const character = characters[characterName];
    if (character.bullets > 0) {
      character.bullets = --character.bullets;
      console.log(
        `${characterName} has successfully hit ${target} and now has ${character.bullets} bullets!`
      );
    } else {
      console.log(
        `${characterName} doesn't have enough bullets to shoot at ${target}!`
      );
    }
  }

  function takeHit(characterName, damage, attacker) {
    const character = characters[characterName];
    if (character.health - damage > 0) {
      character.health -= damage;
      console.log(
        `${characterName} took a hit for ${damage} HP from ${attacker} and now has ${character.health} HP!`
      );
    } else {
      delete characters[characterName];
      console.log(`${characterName} was gunned down by ${attacker}!`);
    }
  }

  function reload(characterName) {
    const character = characters[characterName];
    if (character.bullets < 6) {
      const bulletsToReload = 6 - character.bullets;
      character.bullets = 6;
      console.log(`${characterName} reloaded ${bulletsToReload} bullets!`);
    } else {
      console.log(`${characterName}'s pistol is fully loaded!`);
    }
  }

  function patchUp(characterName, amount) {
    const character = characters[characterName];
    if (character.health === 100) {
      console.log(`${characterName} is in full health!`);
    } else {
      debugger;
      const amountRecovered = Math.min(100 - character.health, amount);
      character.health += amountRecovered;

      console.log(
        `${characterName} patched up and recovered ${amountRecovered} HP!`
      );
    }
  }

  function print() {
    for (const [name, { health, bullets }] of Object.entries(characters)) {
      console.log(name);
      console.log(` HP: ${health}`);
      console.log(` Bullets: ${bullets}`);
    }
  }
}

solve([
  '2',
  'Jesse 100 4',
  'Walt 100 5',
  'FireShot - Jesse - Bandit',
  'TakeHit - Walt - 30 - Bandit',
  'PatchUp - Walt - 20',
  'Reload - Jesse',
  'Ride Off Into Sunset',
]);
