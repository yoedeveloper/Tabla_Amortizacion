using Spectre.Console;
//Aqui se capturan los datos del usuario
decimal montoPrestamo = AnsiConsole.Ask<decimal>("Ingresa el monto del préstamo: ");
decimal interesAnual = AnsiConsole.Ask<decimal>("Ingresa el interés anual: ");
int plazoPrestamo = AnsiConsole.Ask<int>("Indica el plazo del préstamo: ");


//Con esto de forman los parametros de la tala
var tabla = new Table();

tabla.AddColumn("No.");
tabla.AddColumn("Pago de couta");
tabla.AddColumn("Interés a pagar");
tabla.AddColumn("Abono a capital");
tabla.AddColumn("Saldo");

// Esto calcula el interes mensual
decimal interesMensual = interesAnual / 12 / 100;;

decimal divArriba = (decimal)Math.Pow(1 + (double)interesMensual, plazoPrestamo);
decimal divAbajo = (decimal)Math.Pow(1 + (double)interesMensual, plazoPrestamo) - 1;

//Esto calcula coutafija y asigna el valor del saldo
decimal coutaFija = montoPrestamo * interesMensual * (divArriba / divAbajo);
decimal SaldoPrestamo = montoPrestamo;

// Este bucle for se encarga de cargar los valores de la tabla hasta donde se haya elegido el plazo del prestamo
for (int i = 1; i <= plazoPrestamo; i++)
{
    //Estos valores estan dentro del bucle for para que cada vez que se cargue el bucle se actualicen los valores 
    //interes a pagar va disminuyendo con forme disminuya el saldo
    decimal InteresPagar = SaldoPrestamo * interesMensual;

    // El abono al capital va aumentando conforme baje el interes
    decimal AbonoCapital = coutaFija - InteresPagar;

    // El saldo va bajando de acuerdo a cada abono al capital recibido
    SaldoPrestamo = SaldoPrestamo - AbonoCapital;

    // Con esta linea de codigo rellenamos cada uno de los parametros establecidos en la tabla anteriormente
    tabla.AddRow($"{i}", $"{coutaFija:C2}", $"{InteresPagar:C2}", $"{AbonoCapital:C2}", $"{SaldoPrestamo:C2}");

}

// Con esto se imprime toda la tabla
AnsiConsole.Write(tabla);


