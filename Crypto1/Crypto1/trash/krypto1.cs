/*https://habr.com/ru/articles/745820/

func eulerFunc(p, q){
	return (p - 1) * (q - 1);
}
	
func relativelyPrime(a){
	int b;

	for (b = 1; b < a; ++b) {
		if GCD(a, b) == 1:
			return b;
	}
	
	throw new exception("No relatively prime numbers");
}
	
func GCD(num1, num2) {
	int Remainder;
 
    while (num2 != 0)
    {
		Remainder = num1 % num2;
		num1 = num2;
		num2 = Remainder;
	}

	return num1;
}

func euclidExpanded(a, b):
	long q, r, x1 = 0, y2 = 0, x2 = 1, y1 = 1;
	while (b > 0) {
		q = a / b;
		r = a - q * b;
		x = x2 - q * x1;
		y = y2 - q * y1;
		a = b;
		b = r;
		x2 = x1;
		x1 = x;
		y2 = y1;
		y1 = y;
	}
	
	if x2 < y2 return x2;
	return y2;
	
	

func create_key(){
	int p = rand();
	int q = rand();
	
	int N = p * q;
	
	int phi = eulerFunc(p, q);
	
	int e = int(input())#relativelyPrime(phi);
	int d = phi - math.abs(euclidExpanded(phi, e));
	
	openKey = (e, N);
	privateKey = (d, N);
}

func encrypt(text, e, n) {
	return math.pow(text, e) mod n;
}

func decrypt(text, d, n) {
	return math.pow(text, d) mod n;
}

1. Сгенерировать новые ключи
1.1 ввести число e
1.2 создать число e самостоятельно
2. Зашифровать текст
3. Расшифровать текст*/