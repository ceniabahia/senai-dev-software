interface Props {
  titulo: string
  valor: string
  icone: string
  destaque?: boolean   // prop opcional
}

function CardResumo({ titulo, valor, icone, destaque = false }: Props) {
  return (
    <div style={{
      background: destaque ? '#1a3d5c' : 'white',
      color: destaque ? 'white' : '#1a3d5c',
      borderLeft: '6px solid #c8d400',
      borderRadius: 10, padding: 16,
      boxShadow: '0 2px 8px rgba(0,0,0,0.08)'
    }}>
      <div style={{ fontSize: 13, opacity: 0.8 }}>{icone} {titulo}</div>
      <div style={{ fontSize: 26, fontWeight: 800, marginTop: 6 }}>{valor}</div>
    </div>
  )
}
export default CardResumo