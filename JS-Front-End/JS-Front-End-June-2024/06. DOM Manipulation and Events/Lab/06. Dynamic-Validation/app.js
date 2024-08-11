function validate() {
  const regex = RegExp('[a-z]+@[a-z]+.[a-z]+');
  const emailInputElement = document.getElementById('email');

  emailInputElement.addEventListener('change', (e) => {
    e.target.classList.toggle('error', !regex.test(e.target.value));
  });
}
