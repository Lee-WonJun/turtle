(defproject clojureturtle "0.1.0-SNAPSHOT"
  :description "FIXME: write description"
  :url "http://example.com/FIXME"
  :license {:name "EPL-2.0 OR GPL-2.0-or-later WITH Classpath-exception-2.0"
            :url "https://www.eclipse.org/legal/epl-2.0/"}
  :dependencies [[org.clojure/clojure "1.10.0"]
                 [com.google/clojure-turtle "0.3.0"]
                 [org.clojure/core.match "1.0.0"]
                 [org.clojure/algo.monads "0.1.6"]
                 [failjure "2.0.0"]
                 ]
  :main ^:skip-aot clojureturtle.core
  :target-path "target/%s"
  :profiles {:uberjar {:aot :all}})
