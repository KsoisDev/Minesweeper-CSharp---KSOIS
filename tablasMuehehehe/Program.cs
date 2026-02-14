// See https://aka.ms/new-console-template for more information
//TODO: 
// - Ganar poniendo todas las banderas adecuadas
// - Perder
// - Generacion Aleatorias de tabla
//
namespace tablasMuehehehe
{
class Program
{
	static String[,] tabla;
	static String[,] tablaVisible;
    static bool perder = false;
	static void Main(string[] args)
	{
		tabla = pedirMatriz();
		//mostrarMatriz(tabla);
		Console.WriteLine("cuantas minas quieres descubrir");
		int numeroDeMinas = int.Parse(Console.ReadLine());
		for (int i = 0; i < numeroDeMinas; i++)
		{
				Console.WriteLine("Coordenada:");
				String input = Console.ReadLine();
				String[] dimensiones = input.Split(' ');
				int filas = int.Parse(dimensiones[0]) - 1;
				int columnas = int.Parse(dimensiones[1]) - 1;
				DescubrirMatriz(filas, columnas);
				Console.WriteLine(perder);
				Console.WriteLine("Has perdido");
		}
		mostrarMatriz(tablaVisible);
		if(perder) Console.WriteLine("has perdido."); 
		else Console.WriteLine("Pues no has perdido");
	}

	static void DescubrirMatriz(int fila, int columna)
	{
		int numeroMinas = 0;
		if (tabla[fila, columna] == "*")
		{
			tablaVisible[fila, columna] = "*";
			perder = true;
			return;
		}
		for (int i = fila - 1; i <= (fila + 1); i++)
		{
			for (int j = columna - 1; j <= (columna + 1); j++)
			{
				if (i < 0 || j < 0 || i >= tabla.GetLength(0) || j >= tabla.GetLength(1))
				{
					continue;
				}

				// Console.WriteLine($"i: {i}, j: {j}");
				//Console.Write(tabla[i, j]);
				if (tabla[i, j] == "*")
				{
					numeroMinas++;
				}
			}

		//	Console.WriteLine();
		}

		if (numeroMinas > 0)
		{
			//Console.WriteLine($"fila: {fila}, columna: {columna},  numeroMinas: {numeroMinas}");
			tablaVisible[fila, columna] = numeroMinas.ToString();
			tabla[fila, columna] = numeroMinas.ToString();
		}
		else
		{
			tablaVisible[fila, columna] = "-";
			tabla[fila, columna] = "0";
			for (int i = fila - 1; i <= (fila + 1); i++)
			{
				for (int j = columna - 1; j <= (columna + 1); j++)
				{
					if (i < 0 || j < 0 || i >= tabla.GetLength(0) || j >= tabla.GetLength(1))
					{
						continue;
					}
					if(tabla[i, j] != "-")
						continue;
					DescubrirMatriz(i, j);
				}
			} 
		}
	}

	static void mostrarMatriz(String[,] matriz )
	{
		Console.WriteLine();
		for(int i=0; i<matriz.GetLength(0); i++)
		{
			for(int j=0; j<matriz.GetLength(1); j++)
			{
				Console.Write(matriz[i, j]);
			}
			Console.WriteLine();
		}
	}

	static void generarMatrizVisible(int filas, int columnas)
	{
		tablaVisible = new string[filas, columnas];
		for (int i = 0; i < filas; i++)
		{
			for (int j = 0; j < columnas; j++)
			{
				tablaVisible[i, j] = "X";
			}
		}
	}

	static int ValidarMinas(String[,] matriz)
	{
		int numeroDeMinas = 0;
		for (int i = 0; i < matriz.GetLength(0); i++)
		{
			for (int j = 0; j < matriz.GetLength(1); j++)
			{
				if (matriz[i, j] == "*")
					numeroDeMinas++;
			}
		}
		return numeroDeMinas;
	}
	static String[,] pedirMatriz()
	{
		Console.WriteLine("vamo a genera tabla\n introduce dimensiones ej: 2 2");
		String input = Console.ReadLine();
		String[] dimensiones = input.Split(' ');
		int filas = int.Parse(dimensiones[0]);
		int columnas = int.Parse(dimensiones[1]);
		generarMatrizVisible(filas, columnas);
		String[,] matriz = new String[filas, columnas];
		for(int i=0; i<filas; i++)
		{
			string filaInput = Console.ReadLine();
			for(int j=0; j<columnas; j++)
			{
				char[] caracteres = filaInput.ToCharArray();
				if(!(caracteres.GetLength(0)==columnas))
				{
					Console.WriteLine("Error pisha");
					i = filas+1;
					j = columnas+1;
					matriz = new String[filas, columnas];
				}
				else
				{
					matriz[i, j] = caracteres[j].ToString();
				}
			}
		}
		return matriz;
	}
	
}
};

