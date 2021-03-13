-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 13-03-2021 a las 08:10:59
-- Versión del servidor: 10.4.14-MariaDB
-- Versión de PHP: 7.4.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `baseproar`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulos`
--

CREATE TABLE `articulos` (
  `articuloid` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `descripcion` varchar(100) NOT NULL,
  `marca` varchar(100) NOT NULL,
  `precioactual` double(10,2) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `stockmin` int(11) NOT NULL,
  `estado` varchar(10) NOT NULL DEFAULT 'activo'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `articulos`
--

INSERT INTO `articulos` (`articuloid`, `nombre`, `descripcion`, `marca`, `precioactual`, `cantidad`, `stockmin`, `estado`) VALUES
(44, 'destornillador', 'azul', 'marca24', 40.50, 13, 5, 'activo'),
(66, 'martillo', 'negro', 'marca2', 30.00, 0, 5, 'activo'),
(99, 'martillo', 'rojo', 'marca23', 50.50, 18, 5, 'activo');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articuloxproveedor`
--

CREATE TABLE `articuloxproveedor` (
  `articuloid` int(11) NOT NULL,
  `proveedorid` int(11) NOT NULL,
  `costo` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `articuloxproveedor`
--

INSERT INTO `articuloxproveedor` (`articuloid`, `proveedorid`, `costo`) VALUES
(44, 1, 25),
(66, 1, 25),
(99, 1, 30);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE `clientes` (
  `clienteid` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `telefono` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `cuil` varchar(11) NOT NULL,
  `razonsocial` varchar(100) NOT NULL,
  `tipo` varchar(100) NOT NULL,
  `estado` varchar(10) NOT NULL DEFAULT 'activo'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `clientes`
--

INSERT INTO `clientes` (`clienteid`, `nombre`, `apellido`, `telefono`, `email`, `direccion`, `cuil`, `razonsocial`, `tipo`, `estado`) VALUES
(1, 'pablo', 'suarez', '4853216', 'emailemail@hotmail.com', 'moreno 3698', '27378422681', 'social 1', 'consumidor final', 'activo'),
(2, 'martin', 'rodriguez', '4965821', 'emailemail@hotmail.com', 'rivadavia 3695', '27369547851', 'social social', 'responsable inscripto', 'activo');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `configuracion`
--

CREATE TABLE `configuracion` (
  `proximopedido` int(11) NOT NULL DEFAULT 0,
  `NroMaxPaginas` int(11) NOT NULL DEFAULT 10,
  `proximafacturaA` int(11) NOT NULL DEFAULT 0,
  `proximafacturaB` int(11) NOT NULL DEFAULT 0,
  `usuario` varchar(50) NOT NULL,
  `contraseña` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `configuracion`
--

INSERT INTO `configuracion` (`proximopedido`, `NroMaxPaginas`, `proximafacturaA`, `proximafacturaB`, `usuario`, `contraseña`) VALUES
(0, 10, 0, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(1, 10, 0, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(0, 10, 0, 1, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(2, 10, 0, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(0, 10, 1, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(3, 10, 0, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(4, 10, 0, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(0, 10, 2, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(0, 10, 0, 2, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(5, 10, 0, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(0, 10, 3, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(6, 10, 0, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(0, 10, 4, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(7, 10, 0, 0, 'admin', 'pIbvhgmpVHahDBTYUgQvew=='),
(8, 10, 0, 0, '', ''),
(0, 10, 0, 3, '', '');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detallepedido`
--

CREATE TABLE `detallepedido` (
  `articuloid` int(11) NOT NULL,
  `pedidoid` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `preciovendido` double(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `detallepedido`
--

INSERT INTO `detallepedido` (`articuloid`, `pedidoid`, `cantidad`, `preciovendido`) VALUES
(44, 8, 2, 40.50),
(66, 1, 5, 30.00),
(66, 2, 1, 30.00),
(66, 3, 1, 30.00),
(66, 4, 1, 30.00),
(66, 5, 1, 30.00),
(66, 7, 1, 30.00),
(99, 6, 1, 50.50),
(99, 8, 1, 50.50);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `empleados`
--

CREATE TABLE `empleados` (
  `empleadoid` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `telefono` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `cuil` varchar(11) NOT NULL,
  `estado` varchar(10) NOT NULL DEFAULT 'activo'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `empleados`
--

INSERT INTO `empleados` (`empleadoid`, `nombre`, `apellido`, `telefono`, `email`, `direccion`, `cuil`, `estado`) VALUES
(1, 'juan', 'perez', '4269871', 'email96@hotmail.com', 'rivadavia 3695', '23352688421', 'activo');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `facturaciones`
--

CREATE TABLE `facturaciones` (
  `cod_factura` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `importetotal` double(10,2) NOT NULL,
  `clienteid` int(11) NOT NULL,
  `empleadoid` int(11) NOT NULL,
  `metodopago` varchar(20) NOT NULL,
  `pedidoid` int(11) NOT NULL,
  `tipofactura` char(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `facturaciones`
--

INSERT INTO `facturaciones` (`cod_factura`, `fecha`, `importetotal`, `clienteid`, `empleadoid`, `metodopago`, `pedidoid`, `tipofactura`) VALUES
(1, '2018-08-02', 30.00, 2, 1, 'efectivo/contado', 2, 'A'),
(1, '2018-08-02', 150.00, 1, 1, 'efectivo/contado', 1, 'B'),
(2, '2018-08-02', 30.00, 2, 1, 'efectivo/contado', 3, 'A'),
(2, '2018-08-02', 30.00, 1, 1, 'efectivo/contado', 4, 'B'),
(3, '2018-08-02', 30.00, 2, 1, 'efectivo/contado', 5, 'A'),
(3, '2021-03-13', 131.50, 1, 1, 'efectivo/contado', 8, 'B'),
(4, '2018-08-02', 50.50, 2, 1, 'efectivo/contado', 6, 'A');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pedidos`
--

CREATE TABLE `pedidos` (
  `pedidoid` int(11) NOT NULL,
  `nropedido` int(11) NOT NULL,
  `fecha` date NOT NULL,
  `entregado` varchar(10) NOT NULL DEFAULT 'en espera',
  `clienteid` int(11) NOT NULL,
  `estado` varchar(10) NOT NULL DEFAULT 'activo'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `pedidos`
--

INSERT INTO `pedidos` (`pedidoid`, `nropedido`, `fecha`, `entregado`, `clienteid`, `estado`) VALUES
(1, 1, '2018-08-02', 'entregado', 1, 'activo'),
(2, 2, '2018-08-02', 'entregado', 2, 'activo'),
(3, 3, '2018-08-02', 'entregado', 2, 'activo'),
(4, 4, '2018-08-02', 'entregado', 1, 'activo'),
(5, 5, '2018-08-02', 'entregado', 2, 'activo'),
(6, 6, '2018-08-02', 'entregado', 2, 'activo'),
(7, 7, '2020-12-02', 'en espera', 1, 'activo'),
(8, 8, '2021-03-13', 'entregado', 1, 'activo');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proveedores`
--

CREATE TABLE `proveedores` (
  `proveedorid` int(11) NOT NULL,
  `nombrefantasia` varchar(100) NOT NULL,
  `razonsocial` varchar(100) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `telefono` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `cuit` varchar(11) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `estado` varchar(10) NOT NULL DEFAULT 'activo'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `proveedores`
--

INSERT INTO `proveedores` (`proveedorid`, `nombrefantasia`, `razonsocial`, `nombre`, `apellido`, `telefono`, `email`, `cuit`, `direccion`, `estado`) VALUES
(1, 'fantasia 1', 'social0', 'jose', 'perez', '4892854', 'email36@hotmail.com', '27341598621', 'san martin 3698', 'activo');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `articulos`
--
ALTER TABLE `articulos`
  ADD PRIMARY KEY (`articuloid`);

--
-- Indices de la tabla `articuloxproveedor`
--
ALTER TABLE `articuloxproveedor`
  ADD PRIMARY KEY (`articuloid`,`proveedorid`),
  ADD KEY `FK_proveedor_articuloxproveedor` (`proveedorid`);

--
-- Indices de la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`clienteid`);

--
-- Indices de la tabla `detallepedido`
--
ALTER TABLE `detallepedido`
  ADD PRIMARY KEY (`articuloid`,`pedidoid`),
  ADD KEY `FK_pedidos_detallepedido` (`pedidoid`);

--
-- Indices de la tabla `empleados`
--
ALTER TABLE `empleados`
  ADD PRIMARY KEY (`empleadoid`);

--
-- Indices de la tabla `facturaciones`
--
ALTER TABLE `facturaciones`
  ADD PRIMARY KEY (`cod_factura`,`tipofactura`),
  ADD KEY `FK_clientes_facturaciones` (`clienteid`),
  ADD KEY `FK_empleados_facturaciones` (`empleadoid`),
  ADD KEY `FK_facturaciones_pedidos` (`pedidoid`);

--
-- Indices de la tabla `pedidos`
--
ALTER TABLE `pedidos`
  ADD PRIMARY KEY (`pedidoid`),
  ADD KEY `FK_clientes_pedidos` (`clienteid`);

--
-- Indices de la tabla `proveedores`
--
ALTER TABLE `proveedores`
  ADD PRIMARY KEY (`proveedorid`,`cuit`) USING BTREE;

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `clientes`
--
ALTER TABLE `clientes`
  MODIFY `clienteid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `empleados`
--
ALTER TABLE `empleados`
  MODIFY `empleadoid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `pedidos`
--
ALTER TABLE `pedidos`
  MODIFY `pedidoid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT de la tabla `proveedores`
--
ALTER TABLE `proveedores`
  MODIFY `proveedorid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `articuloxproveedor`
--
ALTER TABLE `articuloxproveedor`
  ADD CONSTRAINT `FK_Articulo_articuloxproveedor` FOREIGN KEY (`articuloid`) REFERENCES `articulos` (`articuloid`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_proveedor_articuloxproveedor` FOREIGN KEY (`proveedorid`) REFERENCES `proveedores` (`proveedorid`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `detallepedido`
--
ALTER TABLE `detallepedido`
  ADD CONSTRAINT `FK_articulos_detallepedido` FOREIGN KEY (`articuloid`) REFERENCES `articulos` (`articuloid`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_pedidos_detallepedido` FOREIGN KEY (`pedidoid`) REFERENCES `pedidos` (`pedidoid`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `facturaciones`
--
ALTER TABLE `facturaciones`
  ADD CONSTRAINT `FK_clientes_facturaciones` FOREIGN KEY (`clienteid`) REFERENCES `clientes` (`clienteid`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_empleados_facturaciones` FOREIGN KEY (`empleadoid`) REFERENCES `empleados` (`empleadoid`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `FK_facturaciones_pedidos` FOREIGN KEY (`pedidoid`) REFERENCES `pedidos` (`pedidoid`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `pedidos`
--
ALTER TABLE `pedidos`
  ADD CONSTRAINT `FK_clientes_pedidos` FOREIGN KEY (`clienteid`) REFERENCES `clientes` (`clienteid`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
