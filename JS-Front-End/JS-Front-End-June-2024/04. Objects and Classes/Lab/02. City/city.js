function printCityInfo(cityObj) {
  for (let key in cityObj) {
    if (cityObj.hasOwnProperty(key)) {
      console.log(`${key} -> ${cityObj[key]}`);
    }
  }
}

printCityInfo({
  name: 'Sofia',
  area: 492,
  population: 1238438,
  country: 'Bulgaria',
  postCode: '1000',
});
