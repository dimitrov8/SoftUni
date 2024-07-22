function processHeroes(inputArray) {
  function createHero(name, level, items) {
    return {
      name,
      level: Number(level),
      items: items.split(', '),
      printStats() {
        console.log(`Hero: ${this.name}`);
        console.log(`level => ${this.level}`);
        console.log(`items => ${this.items.join(', ')}`);
      },
    };
  }

  const heroes = inputArray.map((entry) => {
    const [name, level, items] = entry.split(' / ');

    return createHero(name, level, items);
  });

  heroes.sort((a, b) => a.level - b.level).forEach((hero) => hero.printStats());
}

processHeroes([
  'Isacc / 25 / Apple, GravityGun',
  'Derek / 12 / BarrelVest, DestructionSword',
  'Hes / 1 / Desolator, Sentinel, Antara',
]);
