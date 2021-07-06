document.querySelector('#find').addEventListener('click',function(e){
   
    e.preventDefault();
    var loan=document.getElementById('amount').value;
    var income=document.getElementById('income').value;
    var interestRate=document.getElementById('apr').value;
    var tenure=document.getElementById('tenure').value;
   
    var noOfMonths=Math.floor(tenure)*12;
    var monthlyRate=interestRate/(12*100);
    var onePlusR=Math.pow(1+monthlyRate,noOfMonths)
    var denominator=onePlusR-1;
    var emi=(loan*monthlyRate*(onePlusR/denominator)).toPrecision(5);
    var totalAmt=noOfMonths*parseFloat(emi);
    var totalInt=Math.floor(totalAmt-loan);
    var eliIncome=income*0.6;
    var eligibility=((eliIncome/emi)*100000).toFixed(2);
    var intEligibility=Math.floor(eligibility);

    var payableEmi=document.getElementById('emi');
    var payableamount=document.getElementById('totalAmt');
    var payableEligibility=document.getElementById('totalEligible');
    payableEmi.innerHTML=parseFloat(emi);
    payableamount.innerHTML=parseFloat(totalAmt);
    payableEligibility.innerHTML=parseFloat(eligibility);
    
    // for chart
    let pietotal=parseFloat(totalAmt)+parseFloat(intEligibility);
    let piePayableloan=(parseFloat(totalAmt)/pietotal)*360;
    let piePayableEligibility=(parseFloat(intEligibility)/pietotal)*360;
    

    var chart = new CanvasJS.Chart("chartContainer", {
        animationEnabled: true,
        title: {
          text: "Break Up of Total Payments"
        },
        data: [{
          type: "pie",
          startAngle: 240,
          yValueFormatString: "##0.00\"%\"",
          indexLabel: "{label} {y}",
          dataPoints: [
            {y: piePayableloan, label: "Total Loan"},
            {y: piePayableEligibility, label: "Loan Eligibility"},
          ]
        }]
      });
      chart.render();
  
});