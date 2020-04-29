(ns clojureturtle.common
  (:require [clojure-turtle.core :as draw]))

(defrecord Position [x y])
(defn round2 [x] (Double/parseDouble (format "%.2f" x)))

(defn calc-new-position [distance angle current-pos]
  (let [angle-in-rads (* angle (/ Math/PI 180) )
        x0 (:x current-pos)
        y0 (:y current-pos)
        x1 (+ x0 (* distance (Math/cos angle-in-rads)))
        y1 (+ y0 (* distance (Math/sin angle-in-rads)))]
    (Position. (round2 x1) (round2 y1))))

(def inital-position  (Position. 0 0 ))
(def inital-color :Black)
(def inital-pen-state :Down)

(defn draw-line [log old-pos new-pos color] 
  (log (str "Draw " (:x old-pos) "," (:y old-pos) " -> "  (:x new-pos) "," (:y new-pos) " (Color:" color ")") ))