 
    window.onclick = function (event) { $("#component").hide(); }

    function changeprimary() {
      document.getElementById("footer").classList.remove('gradient-custom');
      document.getElementById("footer").classList.remove('gradient-success');
      document.getElementById("footer").classList.add('gradient-primary');



      document.getElementById("navbar").classList.remove('gradient-custom');
      document.getElementById("navbar").classList.remove('gradient-success');
      document.getElementById("navbar").classList.add('gradient-primary');

      document.getElementById("theadserch").classList.remove('gradient-custom');
      document.getElementById("theadserch").classList.remove('gradient-success');
      document.getElementById("theadserch").classList.add('gradient-primary');


      document.getElementById("myTableHead").classList.remove('gradient-custom');
      document.getElementById("myTableHead").classList.remove('gradient-success');
      document.getElementById("myTableHead").classList.add('gradient-primary');

      var buttons = document.querySelectorAll('button');
      buttons.forEach(function (button) {
        button.style.backgroundColor = '#86B6F6'
        //button.classList.remove('btn-default');
        //button.classList.remove('btn-primary bg-olive');
        //button.classList.add('btn-primary bg-blue');
        //button.classList.remove('btn-primary bg-purple');
      });



    }

    function changesuccess() {
      document.getElementById("footer").classList.remove('gradient-custom');
      document.getElementById("footer").classList.remove('gradient-primary');
      document.getElementById("footer").classList.add('gradient-success');

      document.getElementById("navbar").classList.remove('gradient-custom');
      document.getElementById("navbar").classList.remove('gradient-primary');
      document.getElementById("navbar").classList.add('gradient-success');



      document.getElementById("theadserch").classList.remove('gradient-custom');
      document.getElementById("theadserch").classList.remove('gradient-primary');
      document.getElementById("theadserch").classList.add('gradient-success');

      document.getElementById("myTableHead").classList.remove('gradient-custom');
      document.getElementById("myTableHead").classList.remove('gradient-primary');
      document.getElementById("myTableHead").classList.add('gradient-success');

      var buttons = document.querySelectorAll('button');
      buttons.forEach(function (button) {
        button.style.backgroundColor = '#AAD9BB';
        //button.classList.remove('btn-default');
        //button.classList.add('btn-primary bg-olive');
        //button.classList.remove('btn-primary bg-blue');
        // button.classList.remove('btn-primary bg-purple');
      });



    }

    function changecustom() {
      document.getElementById("footer").classList.remove('gradient-primary');
      document.getElementById("footer").classList.remove('gradient-success');
      document.getElementById("footer").classList.add('gradient-custom');

      document.getElementById("navbar").classList.remove('gradient-primary');
      document.getElementById("navbar").classList.remove('gradient-success');
      document.getElementById("navbar").classList.add('gradient-custom');

      document.getElementById("theadserch").classList.remove('gradient-primary');
      document.getElementById("theadserch").classList.remove('gradient-success');
      document.getElementById("theadserch").classList.add('gradient-custom');

      document.getElementById("myTableHead").classList.remove('gradient-primary');
      document.getElementById("myTableHead").classList.remove('gradient-success');
      document.getElementById("myTableHead").classList.add('gradient-custom');


      var buttons = document.querySelectorAll('button');
      buttons.forEach(function (button) {
        button.style.backgroundColor = '#9EA1D4';
        //button.classList.add('btn-primary bg-purple');
      });


    }


    

    
   
    



    
    document.getElementById("AddNew").addEventListener("click", function () {
      document.getElementById("addPartydetails").style.display = "block";
      document.getElementById("inquiryserch").style.display = "none";
      document.getElementById("partyitemsection").style.display = "block";
    });
    
$("#delete-row-button").click(function () {
    var row_count = $("#myTable tbody tr").length;
    if (row_count === 0) {
        Swal.fire({
            title: "No rows to delete",
            icon: "warning",
            confirmButtonText: "OK"
        });
    } else {
        $("#myTable tbody tr:last").remove();
    }
});



    
    document.getElementById("searchButton").addEventListener("click", function () {
      var hiddenSection = document.getElementById("addPartydetails");
      var showSection = document.getElementById("inquiryserch");
      if (showSection.style.display === "none") {
        hiddenSection.style.display = "none";
        showSection.style.display = "block";
        document.getElementById("partyitemsection").style.display = "none";
      }
    });
    

    
    $(document).ready(function () {
      var Username = "SampleUser";
      var PasswordHash = "SamplePasswordHash";
      var base64Credentials = btoa(Username + ":" + PasswordHash);
      var userId = sessionStorage.getItem("userID");
      $.ajax({
        type: 'POST',


          url: 'https://103.80.32.71/testapi/Demo/GetPartyNamed?party_code=' + userId,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        headers: {
          "Authorization": "Basic " + base64Credentials
        },

          success: function (data) {
              console.log(data.party_name);
            document.getElementById("partyname").innerText = data.party_name;
            
        },
        error: function (xhr, textStatus, errorThrown) {

          if (errorThrown === 'net::ERR_CERT_AUTHORITY_INVALID') {
            console.log('SSL certificate authority invalid.');

          }
          else {
            console.log('Other error: ' + errorThrown);
          }
        }


      });



     
    })

   



   

        $(document).ready(function () {
            $("#signout").click(function (event) {
                event.preventDefault();
                Swal.fire({
                    title: "Logout",
                    text: "Are you sure you want to logout?",
                    icon: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#3085d6",
                    cancelButtonColor: "#d33",
                    confirmButtonText: "Yes, logout!",
                    cancelButtonText: "Cancel",
                    allowOutsideClick: false,
                    //showCloseButton:true
                }).then((result) => {
                    if (result.isConfirmed) {
                        Swal.fire({
                            title: "Logged Out",
                            icon: "success",
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,

                            didClose: () => {
                                window.location.href = $("#signout").attr("href");
                            }
                        });
                    }
                });
            })
        })

        $(document).ready(function () {

            //=======script for logout button start =========//
            $("#logoutButton").click(function (event) {
                event.preventDefault(); // Prevent the default click action
                $("#component").show();


            });
            //=======script for logout button end =========//


            function addHoverEffect(elementId) {
                $("#" + elementId).on("mouseover", function () {
                    $(this).css("color", "purple");
                });
                $("#" + elementId).on("mouseout", function () {
                    $(this).css("color", "");
                });
            }
            var mybuttonTop = document.getElementById("myBtnT");

            // When the user scrolls down 20px from the top of the document, show the button
            window.onscroll = function () { scrollFunction() };

            function scrollFunction() {
                if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                    mybuttonTop.style.display = "block";
                } else {
                    mybuttonTop.style.display = "none";
                }
            }


            function topFunction() {
                document.body.scrollTop = 0;
                document.documentElement.scrollTop = 0;
            }






            function showDropdown(menuId) {
                document.getElementById(menuId).classList.add("show");
            }

            function hideDropdown(menuId) {
                document.getElementById(menuId).classList.remove("show");
            }
        });




    
  
    //@if (ViewBag.ShowSweetAlert != null && ViewBag.ShowSweetAlert)
    //{
    //    <script>
    //    $(document).ready(function() {
    //        Swal.fire({
    //            title: 'Error',
    //            text: '@ViewBag.ErrorMessage',
    //            icon: 'error',
    //            confirmButtonText: 'OK'
    //        });
    //    });
    //    </script>
    //}
   
    
    // Retrieve userId from session storage
    var userId = sessionStorage.getItem("userID");

    // Update HTML content with the userId
    // document.getElementById("userIdDisplay").innerText = userId;

    // If you want to set the Party Code with the userId, you can concatenate or use as needed
    document.getElementById("partyCode").innerText = userId;


    document.getElementById("InquiryListBtn").addEventListener("click", function () {
      var userID = userId; // Replace with your actual userId
      window.location.href = '/Enquiry_Quotation/InquiryList?userID=' + userID;
    });

document.getElementById("InquiryListing").addEventListener("click", function () {
    var userID = userId; // Replace with your actual userId
    window.location.href = '/Enquiry_Quotation/InquiryList?userID=' + userID;
});


$(document).ready(function () {
    $('#inquiryListtbl').DataTable();
});
    

  
  
    function readImageFile(file) {
      if (!file) return;

      const reader = new FileReader();
      const promise = new Promise((resolve, reject) => {
        reader.onload = () => resolve(reader.result);
        reader.onerror = reject;
      });
      reader.readAsDataURL(file);
      debugger;
      return promise;
    }


    function saveAllData(tableData) {


      var tableBody = document.getElementById('myTableBody');
      var headers = ["Select", "Party_Enq_Code", "Enq_date", "item_code", "party_item_name", "Dsg_code", "Units", "Size", "Qtydosg_unit_perpack", "No_of_pack", "Total_unit", "Product_Image"];
      var isValid = true;

      // Columns for which validation checks will be applied (adjust column indices accordingly)
      var columnsToValidate = [7, 8, 9, 10];

      var emptyCellCoords = { row: -1, column: -1 };

      //var rowsToSave = [];
      // Iterate through rows
      for (var i = 0; i < tableBody.rows.length; i++) {
        var rowData = {};

        // Iterate through cells in each row
        for (var j = 1; j < tableBody.rows[i].cells.length; j++) {
          if (j === 2 || j === 5) {
            continue;
          }
          var cellContent;
          //var header = tableHead.rows[0].cells[j].innerText;
          // Check if the cell contains a dropdown
          var dropdown = tableBody.rows[i].cells[j].querySelector('select');
          if (dropdown) {
            // Get the selected value from the dropdown
            cellContent = dropdown.value;

            if (!cellContent) {
              isValid = false;
              emptyCellCoords.row = i + 1;
              emptyCellCoords.column = j + 1;
              break; // Exit the loop if validation fails
            }

          } else {
            // Get cell content if it's not a dropdown
            cellContent = tableBody.rows[i].cells[j].innerText;

            if (columnsToValidate.includes(j) && !cellContent.trim()) {
              isValid = false;
              emptyCellCoords.row = i + 1;
              emptyCellCoords.column = j + 1;
              break; // Exit the loop if validation fails
            }
          }

          // Push cell content to the row data array
          //rowData.push(cellContent);

          rowData[headers[j]] = cellContent;

        }



        // Push row data to the table data array
        tableData.push(rowData);

        var jsonData = JSON.stringify(tableData, null, 2);
        console.log(jsonData);
        // console.log(tableData);
        //tableData.push(rowsToSave);
        if (!isValid) {
          break;
        }
      }
      if (isValid) {
        // Save the table data to local storage
        localStorage.setItem('tableData', JSON.stringify(tableData));

        //var requestData = {
        //    Enq_code: tableData[1],
        //    item_code: tableData[3],
        //    Party_item_name: tableData[4],
        //    Dsg_code: tableData[6],
        //    gen_code: tableData[7],
        //    Size: tableData[9],
        //    Units: tableData[8],
        //    Qtydosg_unit_perpack: tableData[10],
        //    No_of_pack: tableData[11],
        //    Total_unit: tableData[12],
        //    Packing_type: tableData[13]
        //    continue for other columns
        //};
        var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = btoa(Username + ":" + PasswordHash);

            $.ajax({
                type: 'POST',
                url: 'https://103.80.32.71/testapi/Demo/InsertIntoUnitMaster',
            data: JSON.stringify(tableData),
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Basic " + base64Credentials
          },
            success: function (data) {
                Swal.fire({
                    icon: 'success',
                    title: 'Success!',
                    text: 'Data saved successfully!' + data,
                });

            //displayNewTable();
            // Handle the success response from the server
            // console.log('Data saved successfully:', data);
          },
            error: function (xhr, textStatus, errorThrown) {

            if (errorThrown === 'net::ERR_CERT_AUTHORITY_INVALID') {
                console.log('SSL certificate authority invalid.');

            }
            else {
                console.log('Other error: ' + errorThrown);
            }
          }
        });


            //console.log('Table data saved to local storage.');
            $('#saveAllButton').prop('disabled', true);
            $('#myTableBody').prop('disabled', true);
        //var jsonData = JSON.stringify(tableData);
        //console.table(tableData);
      }
            else {
                // Display SweetAlert for the first empty cell found
                Swal.fire({
                    icon: 'error',
                    title: 'Error!',
                    text: 'Please fill in the cell in row ' + emptyCellCoords.row + ', column ' + emptyCellCoords.column,
                });

            // Set focus on the first empty cell
            if (emptyCellCoords.row !== -1 && emptyCellCoords.column !== -1) {
                myTableBody.rows[emptyCellCoords.row - 1].cells[emptyCellCoords.column - 1].focus();
        }
      }

    }

            function displayNewTable() {
      // Get data from the original table
      const originalTable = document.getElementById('myTable');
            const rows = originalTable.rows;
            //var tableBody = document.getElementById('myTableBody');
            // Create a new table
            const newTable = document.createElement('table');
            newTable.id = 'newDynamicTable';
            const headerRow = newTable.insertRow(0);

            // Clone header from the original table
            for (let i = 0; i < rows[0].cells.length; i++) {
        const cell = headerRow.insertCell(i);
            cell.innerHTML = rows[0].cells[i].innerHTML;
      }

            // Clone data rows from the original table
            for (let i = 1; i < rows.length; i++) {
        const newRow = newTable.insertRow(i);
            for (let j = 0; j < rows[i].cells.length; j++) {
          var cellContent;
            var dropdown = originalTable.rows[i].cells[j].querySelector('select');
            if (dropdown) {
            // Get the selected value from the dropdown
            const selectedText = originalTable.rows[i].cells[j].querySelector('select option:checked').text;
            cellContent = selectedText;



          } else {
                // Get cell content if it's not a dropdown
                cellContent = originalTable.rows[i].cells[j].innerText;


          }


            const cell = newRow.insertCell(j);
            cell.innerText = cellContent;
          //cell.innerHTML = rows[i].cells[j].innerHTML;
        }
      }

            // Display the new table
            const newTableContainer = document.getElementById('newTableContainer');
            newTableContainer.innerHTML = '';
            newTableContainer.appendChild(newTable);


            document.getElementById('actionButtons').style.display = 'block';
    }


        
           
/*const table = document.getElementById('myTable');*/


           

            function pack(packId) {
      var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = btoa(Username + ":" + PasswordHash);


            // Code to be executed after the delay
            $.ajax({
                type: 'GET',
                url: 'https://103.80.32.71/testapi/Demo/GetPackingTypeList',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Basic " + base64Credentials
        },

            success: function (data) {
          //debugger;
          var ddl_PartyName = $('#' + packId);
            //console.log(ddl_PartyName)
            //ddl_PartyName.empty();

            ddl_PartyName.empty().append($('<option>', {value: '', text: 'Select Packing Type' }));
                $.each(data, function (index, item) {
                    ddl_PartyName.append($('<option>', {
                        value: item.pack_type,
                        text: item.pack_type

                    }));
          });
        },
                error: function (xhr, textStatus, errorThrown) {
          if (errorThrown === 'net::ERR_CERT_AUTHORITY_INVALID') {
                    console.log('SSL certificate authority invalid.');

          } else {
                    console.log('Other error: ' + errorThrown);

          }
        }


      });
    }



























     
    
  











                         
                                function printTable() {
      const newTable = document.getElementById('InquiryItemTable');
                                newTable.classList.add('bordered-table');
                                if (newTable) {
        const printWindow = window.open('', '_blank');
                                printWindow.document.write('<html><head><title>Print</title></head><body>');
                                    printWindow.document.write('<style>.bordered-table {border - collapse: collapse; width: 100%; } th, td {border: 1px solid #dddddd; text-align: left; padding: 8px; }</style>');

                                    printWindow.document.write(newTable.outerHTML);
                                    printWindow.document.write('</body></html>');
                                printWindow.document.close();
                                printWindow.print();
      }
    }
    //function exportToExcel() {
                                    //    const wb = XLSX.utils.table_to_book(document.getElementById('InquiryItemTable'));
                                    //    XLSX.writeFile(wb, 'table_data.xlsx');




                                    function exportToExcel() {
                                        const wb = XLSX.utils.book_new();
                                        const ws = XLSX.utils.table_to_sheet(document.getElementById('InquiryItemTable'));
                                        XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
                                        XLSX.writeFile(wb, 'table_with_images.xlsx');
                                    }


    function exportToPDF() {
      const pdf = new jsPDF();
                                pdf.text('Table Data', 10, 10);
                                pdf.autoTable({html: '#InquiryItemTable' });

                                // Save the PDF
                                pdf.save('table_data.pdf');
    }
                          
                                document.addEventListener('DOMContentLoaded', function () {
      var Username = "SampleUser";
                                var PasswordHash = "SamplePasswordHash";
                                var base64Credentials = btoa(Username + ":" + PasswordHash);


                                // Code to be executed after the delay
                                $.ajax({
                                    type: 'GET',
                                    url: 'https://103.80.32.71/testapi/Demo/GetSectionName',
                                dataType: 'json',
                                contentType: "application/json; charset=utf-8",
                                headers: {
                                    "Authorization": "Basic " + base64Credentials
        },

                                success: function (data) {
          //debugger;
          var ddl_PartyName = $('#Searchinquirydosewise');
                                //console.log(ddl_PartyName)
                                //ddl_PartyName.empty();

                                ddl_PartyName.empty().append($('<option>', {value: '', text: 'Select Dose Form' }));
                                    $.each(data, function (index, item) {
                                        ddl_PartyName.append($('<option>', {
                                            value: item.dsg_code,
                                            text: item.dsg_form

                                        }));
          });
        },
                                    error: function (xhr, textStatus, errorThrown) {

          if (errorThrown === 'net::ERR_CERT_AUTHORITY_INVALID') {
                                        console.log('SSL certificate authority invalid.');

          }
                                    else {
                                        console.log('Other error: ' + errorThrown);
          }
        }


      });
    })
    

                                    let dateObj = new Date();
                                    let month = String(dateObj.getMonth() + 1)
                                    .padStart(2, '0');

                                    let day = String(dateObj.getDate())
                                    .padStart(2, '0');

                                    let year = dateObj.getFullYear();
                                    let output = day + '/' + month + '/' + year;


                                    var userId = sessionStorage.getItem("userID");
                                    window.onload = function () {
                                    var input1 = document.getElementById('input1');
                                    input1.value = userId;
                                    var input3 = document.getElementById('input3');
                                    input3.value = output;





                                    var Username = "SampleUser";
                                    var PasswordHash = "SamplePasswordHash";
                                    var base64Credentials = btoa(Username + ":" + PasswordHash);


                                    // Code to be executed after the delay
                                    $.ajax({
                                        type: 'GET',
                                        url: 'https://103.80.32.71/testapi/Demo/GenerateEnqCode?Party=' + userId,
                                    // this url is for getting current max Inquery Code
                                    //url: 'https://103.80.32.71/testapi/Demo/GetEnquiryQuotation',
                                    dataType: 'json',
                                    contentType: "application/json; charset=utf-8",
                                    headers: {
                                        "Authorization": "Basic " + base64Credentials
        },

                                    success: function (data) {
                                    var input2 = document.getElementById('input2');
                                    input2.value = data;

        },
                                    error: function (xhr, textStatus, errorThrown) {

          if (errorThrown === 'net::ERR_CERT_AUTHORITY_INVALID') {
                                        console.log('SSL certificate authority invalid.');

          }
                                    else {
                                        console.log('Other error: ' + errorThrown);
          }
        }
      });


                                    $.ajax({
                                        type: 'GET',
                                        url: 'https://103.80.32.71/testapi/Demo/GetddlCountryList',
                                    dataType: 'json',
                                    contentType: "application/json; charset=utf-8",
                                    headers: {
                                        "Authorization": "Basic " + base64Credentials
        },

                                    success: function (data) {

          var ddl_PartyName = $('#country');
                                    //console.log(ddl_PartyName)
                                    //ddl_PartyName.empty();

                                    ddl_PartyName.empty().append($('<option>', {value: '', text: 'Select Destination Country' }));
                                        $.each(data, function (index, item) {
                                            ddl_PartyName.append($('<option>', {
                                                value: item.ID,
                                                text: item.coun_name

                                            }));
          });



        },
                                        error: function (xhr, textStatus, errorThrown) {

          if (errorThrown === 'net::ERR_CERT_AUTHORITY_INVALID') {
                                            console.log('SSL certificate authority invalid.');

          }
                                        else {
                                            console.log('Other error: ' + errorThrown);
          }
        }
      });





    };



  
    //this script is for fetching all the data for all,pending,approved,drug dossage,enq_code wise

                                     

                                                            function updateCounterPrice(newRow, rowData) {

      const rowCells = newRow.closest('tr').find('td');
                                                            const inputField = newRow.find('td:nth-last-child(2) input[type="text"]');
                                                            const newValue = inputField.val();
                                                            console.log(newValue);
                                                            console.log(rowData.ItemCode);



                                                            var Username = "SampleUser";
                                                            var PasswordHash = "SamplePasswordHash";
                                                            var base64Credentials = btoa(Username + ":" + PasswordHash);
                                                            $.ajax({
                                                                type: 'PATCH',
                                                                url: 'https://103.80.32.71/testapi/Demo/GetEnqByParty?Counter_price=' + newValue + '&Item_code=' + rowData.ItemCode,
                                                            dataType: 'json',
                                                            contentType: "application/json; charset=utf-8",
                                                            data: JSON.stringify([{
                                                                "Counter_price": newValue,
                                                            "Item_code": rowData.ItemCode
        }]),
                                                            headers: {
                                                                "Authorization": "Basic " + base64Credentials
        },

                                                            success: function (data) {
                                                                Swal.fire({
                                                                    icon: 'success',
                                                                    title: 'Congratulations',
                                                                    text: 'Success!! Update Operation Done Successfully',
                                                                });

        },
                                                            error: function (error) {
                                                                Swal.fire({
                                                                    icon: 'error',
                                                                    title: 'So Sad!!',
                                                                    text: 'Select the Row, Enter Counter Price Then Try Again!',
                                                                });


        }
      });




    }


                                                            function enableEdit(checkbox, row) {
      // console.log(row);
                                                            const rowCells = checkbox.closest('tr').find('td');
                                                            const editableCell = rowCells.eq(rowCells.length - 2);

                                                            if (checkbox.prop('checked')) {
                                                                editableCell.html($('<input>').attr('type', 'text').val(editableCell.text()));
      } //else {
                                                                //    // If you want to revert to the original text when the checkbox is unchecked
                                                                //    editableCell.html(row.data('originalText'));
                                                                //}
                                                            }

                                                            


                                                            // Event listener for dropdown changes
                                                           

                                                           


                                                           

                                                           


    








                                                                    // Event listener for date range button click
                                                                   
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}


function closepanel() {
    $("#Description").hide();
    $("#ItemList").show();
    const tableBody = $('#orderDetail tbody');
    tableBody.empty();
}

$(document).ready(function () {
    var Username = "SampleUser";
    var PasswordHash = "SamplePasswordHash";
    var base64Credentials = btoa(Username + ":" + PasswordHash);
    $.ajax({
        type: 'GET',
        url: 'https://103.80.32.71/testapi/Demo/GetPartyName',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        headers: {
            "Authorization": "Basic " + base64Credentials
        },
        success: function (data) {
            var ddl_PartyName = $("#Searchparty");
            ddl_PartyName.empty().append($('<option>', { value: '', text: 'Select Party Name' }));

            $.each(data, function (index, item) {
                console.log(item.PartyCode, item.PartyName);
                ddl_PartyName.append($('<option>', {
                    value: item.party_code,
                    text: item.party_name
                }));
            });
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error: ' + errorThrown);
        }
    });
});



document.addEventListener('DOMContentLoaded', function () {
    var Username = "SampleUser";
    var PasswordHash = "SamplePasswordHash";
    var base64Credentials = btoa(Username + ":" + PasswordHash);


    // Code to be executed after the delay
    $.ajax({
        type: 'GET',
        url: 'https://103.80.32.71/testapi/Demo/GetSectionName',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        headers: {
            "Authorization": "Basic " + base64Credentials
        },

        success: function (data) {
            console.log(data);
            //debugger;
            var ddl_PartyName = $('#Searchdosewise');
            //console.log(ddl_PartyName)
            //ddl_PartyName.empty();

            ddl_PartyName.empty().append($('<option>', { value: '', text: 'Select Dose Form' }));
            $.each(data, function (index, item) {
                ddl_PartyName.append($('<option>', {
                    value: item.dsg_code,
                    text: item.dsg_form

                }));
            });
        },
        error: function (xhr, textStatus, errorThrown) {

            if (errorThrown === 'net::ERR_CERT_AUTHORITY_INVALID') {
                console.log('SSL certificate authority invalid.');

            }
            else {
                console.log('Other error: ' + errorThrown);
            }
        }
    });
})


$('#Searchparty').change(function () {
    const partyCode = $('#Searchparty').val();
    //const dosageForm = $('#Searchinquirydosewise').val();
    //const startDate = $('#startDate').val();
    //const endDate = $('#endDate').val();
    //$("#divLoader").show();
    getEnqCodePartyWise(partyCode);
});


function getEnqCodePartyWise(partyCode) {
    var Username = "SampleUser";
    var PasswordHash = "SamplePasswordHash";
    var base64Credentials = btoa(Username + ":" + PasswordHash);
    //var userId = sessionStorage.getItem("userID");
    //console.log(userId);
    //var selectedvalue = 'P00026';
    // Code to be executed after the delay
    /*https://103.80.32.71/testapi/Demo/GetEnqCodeByPartyList?party= */
    $.ajax({
        type: 'GET',


        url: 'https://103.80.32.71/testapi/Demo/GetEnqByPartydatalistdata?party=' + partyCode,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        headers: {
            "Authorization": "Basic " + base64Credentials
        },

        success: function (data) {
            //debugger;
            console.log(data);
            var ddl_PartyName = $('#Searchinquiry');
            //console.log(ddl_PartyName)
            //ddl_PartyName.empty();

            //ddl_PartyName.empty().append($('<option>', { value: '', text: 'Select Code' }));
            $.each(data, function (index, item) {
                ddl_PartyName.append($('<option>', {
                    value: item.enq_code,
                    text: item.enq_code

                }));
            });
        },
        error: function (xhr, textStatus, errorThrown) {

            if (errorThrown === 'net::ERR_CERT_AUTHORITY_INVALID') {
                console.log('SSL certificate authority invalid.');

            }
            else {
                console.log('Other error: ' + errorThrown);
            }
        }


    });
}




// Function to update the table with the data





$('#SearchAllpendinginquiry').change(function () {
    const inquiryType = $('#SearchAllpendinginquiry').val();
    //const dosageForm = $('#Searchinquirydosewise').val();
    //const startDate = $('#startDate').val();
    //const endDate = $('#endDate').val();
    $("#divLoader").show();

    if (inquiryType === "All") {
        getDataAll();
    }
    if (inquiryType === "Pending") {
        getDataPending();
    }
    if (inquiryType === "Approved") {
        getDataApproved();
    }



});


// Event listener for dropdown changes
$('#Searchinquiry').change(function () {
    const inquiryCode = $('#Searchinquiry').val();
    //const dosageForm = $('#Searchinquirydosewise').val();
    //const startDate = $('#startDate').val();
    //const endDate = $('#endDate').val();
    $("#divLoader").show();
    getDataEnqCodeWise(inquiryCode);
});

$('#Searchinquirydosewise').change(function () {
    //const inquiryCode = $('#Searchinquiry').val();
    const dosageForm = $('#Searchinquirydosewise').val();
    //const startDate = $('#startDate').val();
    //const endDate = $('#endDate').val();
    $("#divLoader").show();
    getDataDoseWise(dosageForm);
});
// Event listener for date range button click
function getDateRange() {
    // const inquiryCode = $('#Searchinquiry').val();
    //const dosageForm = $('#Searchinquirydosewise').val();
    const startDate = $('#startDate').val();
    const endDate = $('#endDate').val();
    $("#divLoader").show();
    getDataDateWise(startDate, endDate);
}

let row = null;





let selectedRow = null;
function handleCheckbox(checkbox, row) {
    if (checkbox.prop('checked')) {
        // If the checkbox is checked, uncheck the previously selected row
        if (selectedRow) {
            selectedRow.find('input[type="checkbox"]').prop('checked', false);
        }

        // Set the current row as the selected row
        selectedRow = checkbox.closest('tr');
    } else {
        // If the checkbox is unchecked, clear the selected row
        selectedRow = null;
    }

    // Your existing logic to assign values can go here
    enableEdit(checkbox, row);
}


function updateCounterPrice(newRow, rowData) {

    const rowCells = newRow.closest('tr').find('td');
    const inputField = newRow.find('td:nth-last-child(2) input[type="text"]');
    const newValue = inputField.val();
    console.log(newValue);
    console.log(rowData.ItemCode);



    var Username = "SampleUser";
    var PasswordHash = "SamplePasswordHash";
    var base64Credentials = btoa(Username + ":" + PasswordHash);
    $.ajax({
        type: 'PATCH',
        url: 'https://103.80.32.71/testapi/Demo/GetEnqByParty?Counter_price=' + newValue + '&Item_code=' + rowData.ItemCode,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify([{
            "Counter_price": newValue,
            "Item_code": rowData.ItemCode
        }]),
        headers: {
            "Authorization": "Basic " + base64Credentials
        },

        success: function (data) {
            Swal.fire({
                icon: 'success',
                title: 'Congratulations',
                text: 'Success!! Update Operation Done Successfully',
            });

        },
        error: function (error) {
            Swal.fire({
                icon: 'error',
                title: 'So Sad!!',
                text: 'Select the Row, Enter Counter Price Then Try Again!',
            });


        }
    });




}
