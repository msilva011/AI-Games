# Mayara Rosa da Silva

### Relatório Projeto 3 A\* com Waypoints

#### Lista de Links

| Origem | Destino |
| ------ | ------- |
| wp0    | wp1     |
| wp0    | wp18    |
| wp1    | wp2     |
| wp2    | wp13    |
| wp2    | wp3     |
| wp3    | wp4     |
| wp3    | wp5     |
| wp5    | wp7     |
| wp5    | wp9     |
| wp7    | wp8     |
| wp9    | wp21    |
| wp10   | wp8     |
| wp11   | wp10    |
| wp12   | wp11    |
| wp12   | wp13    |
| wp14   | wp12    |
| wp15   | wp14    |
| wp15   | wp16    |
| wp16   | wp6     |
| wp17   | wp15    |
| wp18   | wp17    |
| wp19   | wp18    |
| wp20   | wp19    |
| wp21   | wp20    |

### Relatório: Dificuldades e Soluções no Projeto |

1. Dificuldades encontradas
   1. **Movimento do Player**
      1. **Problema**:
         O player (representado por uma cápsula) não seguia corretamente o caminho dos waypoints. Ele se movia apenas em linha reta e, muitas vezes, caía do cenário.
      2. **Causa**:
         Valor de accuracy muito alto, o que fazia o player considerar que havia alcançado o waypoint antes de realmente chegar ao ponto correto.
         Direção de movimento calculada de forma imprecisa, causando rotações inadequadas.
   
   2. **Velocidade do Player**
      1. **Problema**:
         A velocidade configurada no código era muito alta, dificultando o controle do movimento e a precisão ao atingir os waypoints.
      2.  **Causa**:
         O valor inicial de speed estava acima do necessário para um deslocamento suave.

   3. **Quedas no Cenário**
      1. **Problema**:
         Quando o player saía da rota correta, ele caía do cenário e permanecia fora do campo de interação.
      2. **Causa**:
         Ausência de um sistema de verificação para detectar e corrigir quedas.
   4. **Visualização do Cenario e Movimentação**
         1. **Problema**:
            Poderia ser dificil a visualização de cada ponto de visita e da movimentação do personagem.
         2. **Causa**:
            Devido ao tamanho e quantidade de componentes no cenário a visualização e acompanhamento ficava prejudicada.

2. **Soluções Aplicadas**
      1. **Ajuste da Movimentação do Player**

         Reduzi o valor de accuracy para 0.2f, garantindo maior precisão ao identificar a chegada ao waypoint.
         Adicionei cálculos de direção mais precisos e suavizei as rotações;
      2. **Controle da Velocidade do Player**
   
         Diminuí o valor de speed para 3.0f, proporcionando um deslocamento mais controlado e natural;
      3. **Sistema de Correção para Quedas**
         
         Criado um método CheckFall que reposiciona o player no waypoint inicial ao detectar que ele caiu abaixo de um limite definido.
      4. **Implementação de um Script para a Câmera**
         
         Criado e configurado um script para a câmera seguir o player (não em primeira pessoa):
         A posição é atualizada com base no player, adicionando um offset para um ângulo adequado.
         Adicionado suavização no movimento com Vector3.Lerp.

### Resultados
As soluções aplicadas garantiram que:

- O player segue corretamente os waypoints sem cair prematuramente;
- O movimento está mais suave e preciso;
- A câmera acompanha o player, melhorando a experiência;
- Quedas fora do cenário são tratadas de maneira automática, permitindo a continuidade do jogo.

### Conclusão
As dificuldades enfrentadas foram resolvidas com soluções simples e eficazes. O sistema está agora funcional e pronto para melhorias adicionais.

### [Visualizar Projeto](https://play.unity.com/en/games/ef160d11-dcde-4555-afd8-7b93b734b81f/ancient-world)

-----------------------------------------------------------------------------------------------------------

### REFERENCIAS

#### CASTELO
Fantasy Castle Free 3D Model. Disponível em: https://free3d.com/3d-model/fantasy-castle-40715.html.

#### Texturas
Google Image

#### Escultura
Greek Slave sculpture | CGTrader. Disponível em: https://www.cgtrader.com/items/2484751/download-page.

#### Pirâmides
LOW, F. Future low poly floating pyramid | 3D model. Disponível em: https://www.cgtrader.com/free-3d-models/exterior/historic-exterior/future-low-poly-floating-pyramid.

#### Fonte Chafariz
HORSE. horse fountain | 3D model. Disponível em: <https://www.cgtrader.com/free-3d-models/architectural/other/horse-fountain-608bddf9-a2ff-48e0-a864-1c8d1c2065a6>.

#### Coliseum
KOLIZEY COLISEUM | CGTrader. Disponível em: https://www.cgtrader.com/items/2384401/download-page.