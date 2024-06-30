function isWithinTheSpeedLimit(currentSpeed, area) {
  let speedLimit;
  let status;
  let difference;
  switch (area) {
    case 'motorway':
      speedLimit = 130;
      break;
    case 'interstate':
      speedLimit = 90;
      break;
    case 'city':
      speedLimit = 50;
      break;
    case 'residential':
      speedLimit = 20;
      break;
  }

  if (currentSpeed <= speedLimit) {
    console.log(`Driving ${currentSpeed} km/h in a ${speedLimit} zone`);
    return;
  }
  if (currentSpeed > speedLimit) {
    difference = currentSpeed - speedLimit;
    if (difference <= 20) {
      status = 'speeding';
    } else if (difference <= 40) {
      status = 'excessive speeding';
    } else {
      status = 'reckless driving';
    }
  }

  console.log(
    `The speed is ${difference} km/h faster than the allowed speed of ${speedLimit} - ${status}`
  );
}

let area = 'motorway';
let currentSpeed = 200;

isWithinTheSpeedLimit(currentSpeed, area);
