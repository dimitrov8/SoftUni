function processCats(inputArray) {
  class Cat {
    constructor(name, age) {
      this.name = name;
      this.age = age;
    }

    meow() {
      console.log(`${this.name}, age ${this.age} says Meow`);
    }
  }

  inputArray.forEach((entry) => {
    const [name, age] = entry.split(' ');

    const cat = new Cat(name, parseInt(age, 10));
    cat.meow();
  });
}

processCats(['Mellow 2', 'Tom 5']);
