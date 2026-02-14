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
	static int numeroDeMinas = 0;
    static int minasMarcadas = 0; //minas correctamente marcadas
    static int minasMarcadasTotales = 0; //minas marcadas
	static void Main(string[] args)
	{
		tabla = pedirMatriz();
		//MarcarTabla();
		bool ganar = false;
		while (!ganar && !perder)
		{
			Console.WriteLine("Quieres descubrir o quieres marcar? \n marcar: 1 descubri: 2 desmarcar: 3");
			int input = int.Parse(Console.ReadLine());
			switch (input)
			{
				case 1: MarcarTabla(); break;
				case 2: DescubrirTabla(); break;
				case 3: DesmarcarTabla(); break;
				 default: Console.WriteLine("Error"); break;
			}
			ValidarMinas(tabla, tablaVisible);
			ganar = ValidarVictoria(tablaVisible, numeroDeMinas, minasMarcadas);
		}
       if(ganar)
		Console.WriteLine("has ganado!");
       else Console.WriteLine("Has perdido");
		//DescubrirTabla();
		//mostrarMatriz(tabla);

	}

	static void DesmarcarTabla()
	{
		Console.WriteLine("cuantas minas quieres desmarcar");
		int numeroDeMinas = int.Parse(Console.ReadLine());
		for (int i = 0; i < numeroDeMinas; i++)
		{
			Console.WriteLine("Coordenada:");
			String input = Console.ReadLine();
			String[] dimensiones = input.Split(' ');
			int filas = int.Parse(dimensiones[0]) - 1;
			int columnas = int.Parse(dimensiones[1]) - 1;
			if ((tablaVisible[filas, columnas] == "P"))
			{
				tablaVisible[filas, columnas] = "X";
				minasMarcadas--;
				minasMarcadasTotales--;
			}
			else
			{
				Console.WriteLine("Esta casilla no esta marcada.");
			}
		}
		mostrarMatriz(tablaVisible);
	}

	static void MarcarTabla()
	{
		Console.WriteLine("cuantas minas quieres marcar");
		int numeroDeMinas = int.Parse(Console.ReadLine());
		for (int i = 0; i < numeroDeMinas; i++)
		{
			Console.WriteLine("Coordenada:");
			String input = Console.ReadLine();
			String[] dimensiones = input.Split(' ');
			int filas = int.Parse(dimensiones[0]) - 1;
			int columnas = int.Parse(dimensiones[1]) - 1;
			if (!(tablaVisible[filas, columnas] == "P"))
			{
				tablaVisible[filas, columnas] = "P";
				minasMarcadasTotales++;
				if (tabla[filas, columnas] == "*")
					minasMarcadas++;
			}
			else
			{
				Console.WriteLine("Esta casilla ya esta marcada.");
			}
		}
		mostrarMatriz(tablaVisible);
	}

	static bool ValidarVictoria(String[,] matrizVisible, int minasTotales, int minasMarcadas)
	{
		if (!perder)
		{
			bool todoDescubierto = true;
			for (int i = 0; i < matrizVisible.GetLength(0); i++)
			{
				for(int j = 0; j<matrizVisible.GetLength(1); j++)
				{
					if (matrizVisible[i, j] == "X")
						todoDescubierto = false;
				}
			}

			if (todoDescubierto && minasMarcadas == minasTotales && minasMarcadasTotales == minasTotales)
				return true;
		}

		return false;
	}
	static void DescubrirTabla()
	{
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
		}
		mostrarMatriz(tablaVisible);
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

	static void ValidarMinas(String[,] matriz, String[,] matrizvisible)
	{
		numeroDeMinas = 0;
		for (int i = 0; i < matriz.GetLength(0); i++)
		{
			for (int j = 0; j < matriz.GetLength(1); j++)
			{
				if (matriz[i, j] == "*")
				{
					numeroDeMinas++;
				}
			}
		}
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

