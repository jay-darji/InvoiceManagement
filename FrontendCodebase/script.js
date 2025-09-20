document.addEventListener('DOMContentLoaded', function () {
  fetch('http://localhost:5062/api/invoice')
    .then(resp => resp.json())
    .then(data => {
      let html = '<ul>';

      if (data.item == null) {
        html += `<li>${'Dummy Item'} - $${16.99}</li>`;
      } else {
        data.items.forEach(item => {
          console.log(item);
          html += `<li>${item.name} - $${item.price}</li>`;
        });
      }
      html += '</ul>';
      document.getElementById('invoice-container').innerHTML = html;
    })
    .catch(er => console.error('Failed to load invoice:', er));
});
